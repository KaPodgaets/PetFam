using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.Application.Database;
using PetFam.Application.Extensions;
using PetFam.Application.Interfaces;
using PetFam.Application.SpeciesManagement.Commands.DeleteBreed;
using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;
using PetFam.Domain.Volunteer;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Application.VolunteerManagement.Commands.PetUpdate;

public class PetUpdateHandler
    :ICommandHandler<PetUpdateCommand>
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
    
    public async Task<Result> ExecuteAsync(
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
            return getVolunteerResult;
        var volunteer = getVolunteerResult.Value;
        
        var petId = PetId.Create(command.PetId);
        
        var speciesId = SpeciesId.Create(command.SpeciesAndBreed.SpeciesId);
        var speciesBreed = SpeciesBreed.Create(
            speciesId,
            command.SpeciesAndBreed.BreedId)
                .Value;
        
        var updateResult = volunteer.UpdatePet(
            petId,
            command.NickName,
            command.SpeciesAndBreed,
            )
        // update via volunteer domain model

        // save model through repository

        // log information
        
        return Result.Success();
    }
}