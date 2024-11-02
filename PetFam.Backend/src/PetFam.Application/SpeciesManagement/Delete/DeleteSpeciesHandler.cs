using Microsoft.Extensions.Logging;
using PetFam.Application.VolunteerManagement;
using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;

namespace PetFam.Application.SpeciesManagement.Delete
{
    public class DeleteSpeciesHandler
    {
        private readonly ISpeciesRepository _repository;
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ILogger _logger;

        public DeleteSpeciesHandler(
            ISpeciesRepository repository,
            IVolunteerRepository volunteerRepository,
            ILogger<DeleteSpeciesHandler> logger)
        {
            _repository = repository;
            _volunteerRepository = volunteerRepository;
            _logger = logger;
        }
        public async Task<Result<Guid>> Execute(DeleteSpeciesCommand request,
            CancellationToken cancellationToken = default)
        {
            var existSpeciesResult = await _repository.GetById(SpeciesId.Create(request.Id), cancellationToken);

            if (existSpeciesResult.IsFailure)
                return Result<Guid>.Failure(existSpeciesResult.Error);

            var getVolunteersResult = await _volunteerRepository.GetAllAsync(cancellationToken);
            if (getVolunteersResult.IsFailure)
            {
                return Errors.General.Failure();
            }
            var allExistingSpecies = getVolunteersResult.Value.SelectMany(x => x.Pets.Select(p => p.SpeciesAndBreed.SpeciesId.Value));

            if(allExistingSpecies.Any(p => p == request.Id))
            {
                return Errors.General.DeletionEntityWithRelation();
            }

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
