using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;

namespace PetFam.Application.SpeciesManagement.Create
{
    public class CreateSpeciesHandler
    {
        private readonly ISpeciesRepository _repository;
        private readonly ILogger _logger;

        public CreateSpeciesHandler(
            ISpeciesRepository repository,
            ILogger<CreateSpeciesHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Result<Guid>> Handle(CreateSpeciesRequest request,
            CancellationToken cancellationToken = default)
        {
            var existingSpeciesByNameResult = await _repository.GetByName(
                request.Name,
                cancellationToken);

            if (existingSpeciesByNameResult.IsSuccess)
            {
                return Result<Guid>.Failure(
                    Errors.Volunteer.AlreadyExist(
                        request.Name));
            }

            var speciesCreateResult = Species.Create(
                SpeciesId.NewId(),
                request.Name);

            if (speciesCreateResult.IsFailure)
                return Result<Guid>.Failure(speciesCreateResult.Error);

            var addResult = await _repository.Add(speciesCreateResult.Value, cancellationToken);

            _logger.LogInformation(
                "Created species with {name} with id {id}",
                request.Name,
                addResult.Value);

            return addResult;
        }
    }
}
