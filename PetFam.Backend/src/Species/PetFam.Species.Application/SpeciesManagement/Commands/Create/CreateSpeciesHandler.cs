namespace PetFam.Species.Application.SpeciesManagement.Commands.Create
{
    public class CreateSpeciesHandler:ICommandHandler<Guid,CreateSpeciesCommand>
    {
        private readonly ISpeciesRepository _repository;
        private readonly IValidator<CreateSpeciesCommand> _validator;
        private readonly ILogger _logger;

        public CreateSpeciesHandler(
            ISpeciesRepository repository,
            ILogger<CreateSpeciesHandler> logger,
            IValidator<CreateSpeciesCommand> validator)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
        }
        public async Task<Result<Guid>> ExecuteAsync(CreateSpeciesCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var existingSpeciesByNameResult = await _repository.GetByName(
                command.Name,
                cancellationToken);

            if (existingSpeciesByNameResult.IsSuccess)
            {
                return Errors.Species.AlreadyExist(command.Name).ToErrorList();
            }

            var speciesCreateResult = Species.Create(
                SpeciesId.NewId(),
                command.Name);

            if (speciesCreateResult.IsFailure)
                return Result<Guid>.Failure(speciesCreateResult.Errors);

            var addResult = await _repository.Add(speciesCreateResult.Value, cancellationToken);

            _logger.LogInformation(
                "Created species with {name} with id {id}",
                command.Name,
                addResult.Value);

            return addResult;
        }
    }
}
