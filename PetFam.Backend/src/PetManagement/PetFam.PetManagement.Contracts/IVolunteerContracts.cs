using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.PetManagement.Contracts;

public interface IVolunteerContracts
{
    Task<Result<bool>> IsPetsWithBreedExisting(Guid breedId, CancellationToken cancellationToken = default);
    Task<Result<bool>> IsPetsWithSpeciesExisting(Guid speciesId, CancellationToken cancellationToken = default);
    Task<IEnumerable<string>> GetAllPhotoPaths(CancellationToken cancellationToken = default);
}