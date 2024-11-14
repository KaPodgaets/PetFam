using PetFam.BreedManagement.Application.Database;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.DeleteBreed;

public class DeleteBreedHandler
    :ICommandHandler<DeleteBreedCommand>
{
    private readonly ISpeciesRepository _repository;
    private readonly IReadDbContext _readDbContext;
    private readonly IValidator<DeleteBreedCommand> _validator;
    private readonly ILogger _logger;

    public DeleteBreedHandler(
        ILogger<DeleteBreedHandler> logger,
        IValidator<DeleteBreedCommand> validator,
        ISpeciesRepository repository,
        IReadDbContext readDbContext)
    {
        _logger = logger;
        _validator = validator;
        _repository = repository;
        _readDbContext = readDbContext;
    }
    public async Task<Result> ExecuteAsync(
        DeleteBreedCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);
        
        if (validationResult.IsValid == false)
            return validationResult.ToErrorList();
        
        var isPetsWithDeletingBreedExist = await _readDbContext.Pets
            .AnyAsync(p => p.SpeciesAndBreed.BreedId == command.BreedId, cancellationToken);
        
        if(isPetsWithDeletingBreedExist)
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