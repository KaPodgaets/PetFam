using PetFam.BreedManagement.Contracts;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Presentation;

public class BreedManagementContracts:IBreedManagementContracts
{
    public Task<Result<bool>> CheckBreedExists(SpeciesId speciesId, BreedId breedId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}