using PetFam.Application.SpeciesManagement.Queries.GetBreeds;

namespace PetFam.Api.Requests.Breed;

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