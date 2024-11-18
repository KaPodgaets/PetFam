using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Files.Contracts;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.Messaging;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.AddPetPhotos
{
    public class PetAddPhotosHandler:ICommandHandler<string, PetAddPhotosCommand>
    {
        private readonly IVolunteerRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PetAddPhotosCommand> _validator;
        private readonly ILogger _logger;
        private readonly IMessageQueue _queue;
        private readonly IFilesContracts _filesContracts;

        public PetAddPhotosHandler(
            IVolunteerRepository repository,
            IUnitOfWork unitOfWork,
            ILogger<PetAddPhotosHandler> logger,
            IMessageQueue queue,
            IValidator<PetAddPhotosCommand> validator,
            IFilesContracts filesContracts)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _queue = queue;
            _validator = validator;
            _filesContracts = filesContracts;
        }

        public async Task<Result<string>> ExecuteAsync(PetAddPhotosCommand command, CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

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
                    var createPetPhotoResult = PetPhoto.Create(fileData.FileMetadata.ObjectName);
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
                var uploadResult = await _filesContracts.UploadFilesAsync(command.Content, cancellationToken);

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
