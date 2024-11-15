using PetFam.Shared.Dtos;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.BreedManagement.Contracts;

public interface IBreedManagementContracts
{
    Task<Result<BreedDto>> GetBreedById(Guid breedId, CancellationToken cancellationToken = default);
}