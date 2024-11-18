using PetFam.BreedManagement.Application.SpeciesManagement.Queries.GetBreeds;

namespace PetFam.BreedManagement.Presentation.Requests;

public record GetBreedsFilteredWithPaginationRequest(
    Guid SpeciesId,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetBreedsFilteredWIthPaginationQuery ToQuery()
    {
        return new GetBreedsFilteredWIthPaginationQuery(
            SpeciesId,
            SortBy,
            SortDirection,
            Page,
            PageSize);
    }
}