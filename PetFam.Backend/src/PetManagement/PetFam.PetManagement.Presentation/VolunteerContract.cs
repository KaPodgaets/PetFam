using PetFam.PetManagement.Contracts;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.PetManagement.Presentation;

public class VolunteerContract : IVolunteerContract
{
    public Task<Result<bool>> IsPetsWithBreedExisting(BreedId breedId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> IsPetsWithSpeciesExisting(SpeciesId speciesId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}