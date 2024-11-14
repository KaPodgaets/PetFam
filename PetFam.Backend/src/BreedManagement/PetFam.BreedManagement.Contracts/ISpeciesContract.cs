using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Contracts;

public interface ISpeciesContract
{
    Task<Result<bool>> CheckBreedExists(SpeciesId speciesId, BreedId breedId, CancellationToken cancellationToken = default);
}