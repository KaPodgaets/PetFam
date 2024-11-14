using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Application.FileManagement;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.Messaging;
using PetFam.Shared.Shared;
using PetFam.Shared.ValueObjects.Volunteer;

namespace PetFam.Application.VolunteerManagement.Commands.DeletePet;

public class DeletePetHandler
    :ICommandHandler<Guid,DeletePetCommand>
{
    private readonly IVolunteerRepository _repository;
    private readonly ILogger<DeletePetHandler> _logger;
    private readonly IValidator<DeletePetCommand> _validator;
    private readonly IMessageQueue _queue;

    public DeletePetHandler(
        IVolunteerRepository repository,
        ILogger<DeletePetHandler> logger,
        IValidator<DeletePetCommand> validator,
        IMessageQueue queue)
    {
        _repository = repository;
        _logger = logger;
        _validator = validator;
        _queue = queue;
    }

    public async Task<Result<Guid>> ExecuteAsync(
        DeletePetCommand command,
        CancellationToken cancellationToken = default)
    {
        //validation
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();
        
        // get domain models
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var getVolunteerResult = await _repository.GetById(volunteerId, cancellationToken);
        if (getVolunteerResult.IsFailure)
            return getVolunteerResult.Errors;

        var volunteer = getVolunteerResult.Value;
        var pet = volunteer.Pets.FirstOrDefault(x => x.Id.Value == command.PetId);
        if (pet is null)
            return Errors.General.NotFound(command.PetId.ToString()).ToErrorList();
        
        
        // save file paths to add 
        var photoPaths = pet.Photos.Select(x => x.FilePath).ToArray();
        
        // soft deletion
        volunteer.DeletePet(pet);
        
        // save in DB
        var saveResult = await _repository.Update(volunteer, cancellationToken);
        if (saveResult.IsFailure)
            return saveResult;
        
        // taking care about photos (files)
        if (pet.Photos.Count > 0)
            await _queue.WriteAsync(photoPaths,cancellationToken);
        
        return command.PetId;
    }
}