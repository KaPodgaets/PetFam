using PetFam.BreedManagement.Application.SpeciesManagement.Queries.GetBreedById;
using PetFam.BreedManagement.Contracts;
using PetFam.Shared.Dtos;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.BreedManagement.Presentation;

public class BreedManagementContracts:IBreedManagementContracts
{
    private readonly GetBreedByIdHandler _getBreedByIdHandler;

    public BreedManagementContracts(
        GetBreedByIdHandler getBreedByIdHandler)
    {
        _getBreedByIdHandler = getBreedByIdHandler;
    }

    public async Task<Result<BreedDto>> GetBreedById(
        Guid breedId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetBreedByIdQuery(
            breedId);
        
        var getBreedResult = await _getBreedByIdHandler.HandleAsync(query, cancellationToken);
        if (getBreedResult.IsFailure)
            return getBreedResult;
        
        return getBreedResult.Value;
    }
}