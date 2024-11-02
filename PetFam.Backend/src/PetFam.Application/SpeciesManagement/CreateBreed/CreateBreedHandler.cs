using Microsoft.Extensions.Logging;
using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;

namespace PetFam.Application.SpeciesManagement.CreateBreed
{
    public class CreateBreedHandler
    {
        private readonly ISpeciesRepository _repository;
        private readonly ILogger _logger;

        public CreateBreedHandler(
            ISpeciesRepository repository,
            ILogger<CreateBreedHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<Result<Guid>> Execute(CreateBreedCommand request,
            CancellationToken cancellationToken = default)
        {
            var existSpeciesResult = await _repository.GetById(SpeciesId.Create(request.SpeciesId), cancellationToken);

            if (existSpeciesResult.IsFailure)
                return Result<Guid>.Failure(existSpeciesResult.Error);

            var species = existSpeciesResult.Value;

            var createBreedResult = Breed.Create(BreedId.NewId(), request.Name);

            if (createBreedResult.IsFailure)
                return Result<Guid>.Failure(createBreedResult.Error);

            var addBreedResult = species.AddBreed(createBreedResult.Value);

            if (addBreedResult.IsFailure)
                return Result<Guid>.Failure(addBreedResult.Error);

            await _repository.Update(species, cancellationToken);

            _logger.LogInformation(
                "Add breed with {breedName} and {breedId} to species with id {speciesId}",
                request.Name,
                createBreedResult.Value.Id.Value,
                request.SpeciesId);

            return addBreedResult;
        }
    }
}
