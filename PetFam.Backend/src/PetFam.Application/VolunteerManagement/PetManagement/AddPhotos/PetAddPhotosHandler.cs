using Microsoft.Extensions.Logging;
using PetFam.Application.FileManagement;
using PetFam.Application.FileProvider;
using PetFam.Application.VolunteerManagement.PetManagement.Create;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Application.VolunteerManagement.PetManagement.AddPhotos
{
    public class PetAddPhotosHandler
    {
        private readonly IVolunteerRepository _repository;
        private readonly IFileProvider _fileProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IFilesCleanerMessageQueue _queue;

        public PetAddPhotosHandler(
            IVolunteerRepository repository,
            IFileProvider fileProvider,
            IUnitOfWork unitOfWork,
            ILogger<PetAddPhotosHandler> logger,
            IFilesCleanerMessageQueue queue)
        {
            _repository = repository;
            _fileProvider = fileProvider;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _queue = queue;
        }

        public async Task<Result<string>> Execute(PetAddPhotosCommand command, CancellationToken cancellationToken = default)
        {
            var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

            try
            {
                // create VOs
                var volunteerId = VolunteerId.Create(command.VolunteerId);
                var getVolunteerResult = await _repository.GetById(volunteerId, cancellationToken);

                if (getVolunteerResult.IsFailure)
                {
                    return getVolunteerResult.Errors;
                }

                var volunteer = getVolunteerResult.Value;
                var pet = volunteer.Pets.FirstOrDefault(x => x.Id.Value == command.PetId);

                if (pet == null)
                {
                    return Errors.General.NotFound($"pet with Id {command.PetId} not founded").ToErrorList();
                }

                var galleryItems = new List<PetPhoto>();

                foreach (var fileData in command.Content.FilesData)
                {
                    var createPetPhotoResult = PetPhoto.Create(fileData.FileMetadata.ObjectName, false);
                    if (createPetPhotoResult.IsFailure)
                    {
                        return createPetPhotoResult.Errors;
                    }

                    galleryItems.Add(createPetPhotoResult.Value);
                }

                var result = pet.AddPhotos(galleryItems);

                if (result.IsFailure)
                {
                    return result.Errors;
                    //delete uploaded files in minio in case of failure
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);


                // upload files
                var uploadResult = await _fileProvider.UploadFiles(command.Content, cancellationToken);

                if (uploadResult.IsFailure)
                    return uploadResult.Errors;

                transaction.Commit();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed add Photos with error");

                transaction.Rollback();

                var filesPath = command.Content.FilesData.Select(x => x.FileMetadata.ObjectName).ToArray();

                await _queue.WriteAsync(filesPath, cancellationToken);

                return Error.Failure("upload.photo.error", "Can not add photos to pet").ToErrorList();
            }
            
            
            return "photos uploaded";
        }
    }
}
