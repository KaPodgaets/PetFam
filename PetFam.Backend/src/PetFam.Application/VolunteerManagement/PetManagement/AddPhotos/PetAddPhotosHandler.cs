using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public PetAddPhotosHandler(
            IVolunteerRepository repository,
            IFileProvider fileProvider,
            ILogger<CreatePetHandler> logger)
        {
            _repository = repository;
            _fileProvider = fileProvider;
            _logger = logger;
        }

        public async Task<Result<string>> Execute(PetAddPhotosCommand command, CancellationToken cancellationToken = default)
        {
            // create VOs
            var volunteerId = VolunteerId.Create(command.VolunteerId);
            var getVolunteerResult = await _repository.GetById(volunteerId, cancellationToken);

            if (getVolunteerResult.IsFailure)
            {
                return getVolunteerResult.Error;
            }

            var volunteer = getVolunteerResult.Value;
            var pet = volunteer.Pets.FirstOrDefault(x => x.Id.Value == command.PetId);

            if(pet == null)
            {
                return Errors.General.NotFound($"pet with Id {command.PetId} not founded");
            }

            // upload files
            foreach (var fileData in command.FilesData)
            {
                var uploadResult = await _fileProvider.UploadFile(fileData);

                if (uploadResult.IsFailure)
                {
                    return uploadResult.Error;
                }
            }

            var galleryItems = new List<PetPhoto>();
            foreach(var fileData in command.FilesData)
            {
                var createPetPhotoResult = PetPhoto.Create(fileData.FileMetadata.ObjectName, false);
                if (createPetPhotoResult.IsFailure)
                {
                    return createPetPhotoResult.Error;
                }

                galleryItems.Add(createPetPhotoResult.Value);
            }

            var result = pet.AddPhotos(galleryItems);

            if (result.IsFailure)
            {
                return result.Error;
                //delete uploaded files in minio in case of failure
            }

            var updateResult = await _repository.Update(volunteer);

            if (updateResult.IsFailure)
            {
                return updateResult.Error;
            }
            
            
            return "executed";
        }


    }
}
