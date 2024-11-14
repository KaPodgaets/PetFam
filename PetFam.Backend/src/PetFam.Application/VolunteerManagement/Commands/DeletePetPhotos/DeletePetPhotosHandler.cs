using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Application.Database;
using PetFam.Application.Extensions;
using PetFam.Application.FileManagement;
using PetFam.Application.FileProvider;
using PetFam.Application.Interfaces;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Application.VolunteerManagement.Commands.DeletePetPhotos;

public class DeletePetPhotosHandler
    : ICommandHandler<string[], DeletePetPhotosCommand>
{
    private readonly ILogger<DeletePetPhotosHandler> _logger;
    private readonly IVolunteerRepository _repository;
    private readonly IValidator<DeletePetPhotosCommand> _validator;
    private readonly IFilesCleanerMessageQueue _queue;
    private readonly IFileProvider _fileProvider;

    public DeletePetPhotosHandler(
        ILogger<DeletePetPhotosHandler> logger,
        IVolunteerRepository repository,
        IValidator<DeletePetPhotosCommand> validator,
        IFilesCleanerMessageQueue queue,
        IFileProvider fileProvider)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
        _queue = queue;
        _fileProvider = fileProvider;
    }

    public async Task<Result<string[]>> ExecuteAsync(
        DeletePetPhotosCommand command,
        CancellationToken cancellationToken = default)
    {
        // validation
        var validationResult = await _validator.ValidateAsync(command);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();

        // get volunteer

        var getVolunteerResult = await _repository
            .GetById(
                VolunteerId.Create(command.VolunteerId),
                cancellationToken);
        if (getVolunteerResult.IsFailure)
            return getVolunteerResult.Errors;

        // crete PetPhotos and delete photo from pet in domain
        var photos = command.Paths
            .Select(x => PetPhoto.Create(x).Value)
            .ToList();

        var volunteer = getVolunteerResult.Value;
        var pet = volunteer.Pets.FirstOrDefault(p => p.Id.Value == command.PetId);
        if (pet is null)
            return Errors.General.NotFound("Pet not found").ToErrorList();
        pet.DeletePhotos(photos);

        // update volunteer in DB (use Unit of Work)?
        var updateResult = await _repository.Update(volunteer, cancellationToken);
        if(updateResult.IsFailure)
            return updateResult.Errors;
        
        // delete photos straight if save was successful
        var filesMetadata = command.Paths
            .Select(path => new FileMetadata(Constants.FileManagementOptions.PHOTO_BUCKET, path))
            .ToList();
        
        try
        {
            foreach (var file in filesMetadata)
            {
                await _fileProvider.DeleteFile(file, cancellationToken);
            }
        }
        // add photos to queue for background cleaner if deletion goes wrong for any reason
        catch (Exception ex)
        {
            _logger.LogError("{message}", ex.Message);
            _logger.LogInformation("due to error while deleting photos, photos added into background deletion");
            await _queue.WriteAsync(command.Paths.ToArray(), cancellationToken);
        }

        _logger.LogInformation("Photos were deleted successfully");
        return command.Paths.ToArray();
    }
}