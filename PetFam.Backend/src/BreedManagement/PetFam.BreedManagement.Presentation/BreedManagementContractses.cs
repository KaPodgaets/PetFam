using PetFam.BreedManagement.Contracts;
using PetFam.BreedManagement.Presentation.Requests;
using PetFam.Shared.Dtos;
using PetFam.Shared.Models;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.BreedManagement.Presentation;

public class BreedManagementContractses:IBreedManagementContracts
{
    public Task<Result<bool>> CheckBreedExists(SpeciesId speciesId, BreedId breedId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}