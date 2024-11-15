using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.BreedManagement.Application.Database;
using PetFam.PetManagement.Contracts;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.DeleteBreed;

public class DeleteBreedHandler
    :ICommandHandler<DeleteBreedCommand>
{
    private readonly ISpeciesRepository _repository;
    private readonly IValidator<DeleteBreedCommand> _validator;
    private readonly IVolunteerContracts _volunteerContracts;
    private readonly ILogger _logger;

    public DeleteBreedHandler(
        ILogger<DeleteBreedHandler> logger,
        IValidator<DeleteBreedCommand> validator,
        ISpeciesRepository repository,
        IVolunteerContracts volunteerContracts)
    {
        _logger = logger;
        _validator = validator;
        _repository = repository;
        _volunteerContracts = volunteerContracts;
    }
    public async Task<Result> ExecuteAsync(
        DeleteBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var isPetsWithDeletingBreedExist = await _volunteerContracts
            .IsPetsWithBreedExisting(BreedId.Create(command.BreedId), cancellationToken);
        
        if(isPetsWithDeletingBreedExist.Value)
            return Errors.Breed
                .CannotDeleteDueToRelatedRecords(command.BreedId)
                .ToErrorList();
        
        _logger.LogInformation("Deleting Breed {Breed}", command.BreedId);
        var getSpeciesResult = await _repository
            .GetById(SpeciesId.Create(command.SpeciesId), cancellationToken);
            
        if(getSpeciesResult.IsFailure)
            return Errors.General.NotFound(getSpeciesResult.Value.Id.Value).ToErrorList();
            
        var species = getSpeciesResult.Value;
        var deletingResult = species.DeleteBreed(command.BreedId);

        if (deletingResult.IsFailure)
            return deletingResult;
        
        var savingResult = await _repository.Update(species, cancellationToken);
        
        if (savingResult.IsFailure)
            return savingResult;
        
        _logger.LogInformation("Breed {Breed} deleted", command.BreedId);

        return Result.Success();
    }
}