using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;

namespace PetFam.Application.SpeciesManagement.Delete
{
    public class DeleteSpeciesHandler
    {
        private readonly ISpeciesRepository _repository;
        private readonly ILogger _logger;

        public DeleteSpeciesHandler(
            ISpeciesRepository repository,
            ILogger<DeleteSpeciesHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Result<Guid>> Handle(DeleteSpeciesRequest request,
            CancellationToken cancellationToken = default)
        {
            var existSpeciesResult = await _repository.GetById(SpeciesId.Create(request.Id), cancellationToken);

            if (existSpeciesResult.IsFailure)
                return Result<Guid>.Failure(existSpeciesResult.Error);

            var deleteResult = await _repository.Delete(existSpeciesResult.Value, cancellationToken);

            if (deleteResult.IsFailure)
                return Result<Guid>.Failure(deleteResult.Error);

            _logger.LogInformation(
                "Delete species with {name} with id {id}",
                existSpeciesResult.Value.Name,
                existSpeciesResult.Value.Id.Value);

            return deleteResult;
        }
    }
}
