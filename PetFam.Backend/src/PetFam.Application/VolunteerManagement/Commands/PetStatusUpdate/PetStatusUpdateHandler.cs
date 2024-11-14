using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.Shared;
using PetFam.Shared.ValueObjects.Pet;
using PetFam.Shared.ValueObjects.Volunteer;

namespace PetFam.Application.VolunteerManagement.Commands.PetStatusUpdate;

public class PetStatusUpdateHandler
    : ICommandHandler<Guid, PetStatusUpdateCommand>
{
    private readonly IVolunteerRepository _repository;
    private readonly IValidator<PetStatusUpdateCommand> _validator;
    private readonly ILogger<PetStatusUpdateHandler> _logger;

    public PetStatusUpdateHandler(
        IVolunteerRepository repository,
        IValidator<PetStatusUpdateCommand> validator,
        ILogger<PetStatusUpdateHandler> logger)
    {
        _repository = repository;
        _validator = validator;
        _logger = logger;
    }


    public async Task<Result<Guid>> ExecuteAsync(
        PetStatusUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        // validation
        var validationResult = await _validator.ValidateAsync(command);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();
        
        // get volunteer and pet models
        var volunteerId = VolunteerId.Create(command.VolunteerId);
        var getVolunteerResult = await _repository.GetById(volunteerId, cancellationToken);
        if (getVolunteerResult.IsFailure)
            return getVolunteerResult.Errors;
        
        var volunteer = getVolunteerResult.Value;
        var pet = volunteer.Pets.FirstOrDefault(x => x.Id.Value == command.PetId);
        
        if(pet is null) 
            return Errors.General.NotFound($"Pet with id {command.PetId} not found").ToErrorList();
                
        // update status
        volunteer.UpdatePetStatus(pet, (PetStatus)command.NewPetStatus);
        
        // save changes
        var saveResult = await _repository.Update(volunteer, cancellationToken);
        if(saveResult.IsFailure)
            return saveResult;

        return command.PetId;
    }
}