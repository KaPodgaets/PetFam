using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.PetManagement.Contracts;

public interface IVolunteerContract
{
    Task<Result<bool>> IsPetsWithBreedExisting(BreedId breedId, CancellationToken cancellationToken = default);
    Task<Result<bool>> IsPetsWithSpeciesExisting(SpeciesId speciesId, CancellationToken cancellationToken = default);
}