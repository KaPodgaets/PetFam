using PetFam.BreedManagement.Application.Database;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.Delete
{
    public class DeleteSpeciesHandler:ICommandHandler<Guid, DeleteSpeciesCommand>
    {
        private readonly ISpeciesRepository _repository;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly IValidator<DeleteSpeciesCommand> _validator;
        private readonly ILogger _logger;

        public DeleteSpeciesHandler(
            ISpeciesRepository repository,
            IVolunteerRepository volunteerRepository,
            ILogger<DeleteSpeciesHandler> logger,
            IValidator<DeleteSpeciesCommand> validator,
            IReadDbContext readDbContext)
        {
            _repository = repository;
            _volunteerRepository = volunteerRepository;
            _logger = logger;
            _validator = validator;
            _readDbContext = readDbContext;
        }
        public async Task<Result<Guid>> ExecuteAsync(DeleteSpeciesCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var existSpeciesResult = await _repository.GetById(SpeciesId.Create(command.Id), cancellationToken);
            
            if (existSpeciesResult.IsFailure)
                return Result<Guid>.Failure(existSpeciesResult.Errors);
            
            var species = existSpeciesResult.Value;
            
            // check pets of this species exist wiht ReadDBContext
            
            var isPetsWithDelitingSpeciesExist = await _readDbContext.Pets
                .AnyAsync(p => p.SpeciesAndBreed.SpeciesId == command.Id, cancellationToken);
            
            if(isPetsWithDelitingSpeciesExist)
                return Errors.Species.CannotDeleteDueToRelatedRecords(command.Id).ToErrorList();
            
            species.Delete(); // breeds deleted also - cascade

            var deleteResult = await _repository.Update(species, cancellationToken);

            if (deleteResult.IsFailure)
                return Result<Guid>.Failure(deleteResult.Errors);

            _logger.LogInformation(
                "Delete species with {name} with id {id} with all breeds",
                existSpeciesResult.Value.Name,
                existSpeciesResult.Value.Id.Value);

            return deleteResult;
        }
    }
}
