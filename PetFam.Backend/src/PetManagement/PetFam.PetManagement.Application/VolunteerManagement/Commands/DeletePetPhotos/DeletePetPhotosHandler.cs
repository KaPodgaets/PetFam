using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.Messaging;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.DeletePetPhotos;

public class DeletePetPhotosHandler
    :ICommandHandler<string[],DeletePetPhotosCommand>
{
    private readonly ILogger<DeletePetPhotosHandler> _logger;
    private readonly IVolunteerRepository _repository;
    private readonly IValidator<DeletePetPhotosCommand> _validator;
    private readonly  IMessageQueue _queue;

    public DeletePetPhotosHandler(
        ILogger<DeletePetPhotosHandler> logger,
        IVolunteerRepository repository,
        IValidator<DeletePetPhotosCommand> validator,
        IMessageQueue queue)
    {
        _logger = logger;
        _repository = repository;
        _validator = validator;
        _queue = queue;
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
        if(pet is null)
            return Errors.General.NotFound("Pet not found").ToErrorList();
        pet.DeletePhotos(photos);
        
        // update volunteer in DB (use Unit of Work)?
        var updateResult = await _repository.Update(volunteer, cancellationToken);

        // add photos into queue if save success
        await _queue.WriteAsync(command.Paths.ToArray(), cancellationToken);
        
        _logger.LogInformation("Photos added for deletion");
        return command.Paths.ToArray();
    }
}