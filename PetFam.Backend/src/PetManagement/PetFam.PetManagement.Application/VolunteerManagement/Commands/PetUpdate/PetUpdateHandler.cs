using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;
using PetFam.Shared.SharedKernel.ValueObjects.Species;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.PetUpdate;

public class PetUpdateHandler
    :ICommandHandler<Guid, PetUpdateCommand>
{
    private readonly IVolunteerRepository _repository;
    private readonly IValidator<PetUpdateCommand> _validator;
    private readonly ILogger<PetUpdateHandler> _logger;
    
    public PetUpdateHandler(
        IValidator<PetUpdateCommand> validator,
        IVolunteerRepository repository,
        ILogger<PetUpdateHandler> logger)
    {
        _validator = validator;
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<Result<Guid>> ExecuteAsync(
        PetUpdateCommand command,
        CancellationToken cancellationToken = default)
    {
        // validate
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();
        
        // check species and breed exists
        var getVolunteerResult = await _repository
            .GetById(VolunteerId.Create(command.VolunteerId), cancellationToken);
        if (getVolunteerResult.IsFailure)
            return getVolunteerResult.Errors;
        
        var volunteer = getVolunteerResult.Value;
        
        var petId = PetId.Create(command.PetId);
        
        var speciesId = SpeciesId.Create(command.SpeciesAndBreed.SpeciesId);
        var speciesBreed = SpeciesBreed.Create(
                speciesId,
                command.SpeciesAndBreed.BreedId)
            .Value;
        
        var generalInfo = PetGeneralInfo.Create(
                command.GeneralInfo.Comment,
                command.GeneralInfo.Color,
                command.GeneralInfo.Weight,
                command.GeneralInfo.Height,
                command.GeneralInfo.PhoneNumber)
            .Value;

        var healthInfo = PetHealthInfo.Create(
                command.HealthInfo.Comment,
                command.HealthInfo.IsCastrated,
                command.HealthInfo.BirthDate,
                command.HealthInfo.IsVaccinated,
                command.HealthInfo.Age)
            .Value;

        var address = Address.Create(
                command.Address.Country,
                command.Address.City,
                command.Address.Street,
                command.Address.Building,
                command.Address.Litteral)
            .Value;

        var accountInfo = AccountInfo.Create(
                command.AccountInfo.Number,
                command.AccountInfo.BankName)
            .Value;

        var updateResult = volunteer.UpdatePet(
            petId,
            command.NickName,
            speciesBreed,
            (PetStatus)command.Status,
            generalInfo,
            healthInfo,
            address,
            accountInfo);
        
        if(updateResult.IsFailure)
            return updateResult.Errors;
        
        var saveResult = await _repository.Update(volunteer, cancellationToken);
        if(saveResult.IsFailure)
            return saveResult;
        
        // log information
        _logger.LogInformation("pet {PetId} was updated",
            command.PetId);

        return command.PetId;
    }
}