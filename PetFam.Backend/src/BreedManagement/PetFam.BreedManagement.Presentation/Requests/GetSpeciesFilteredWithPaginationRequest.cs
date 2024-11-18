using PetFam.BreedManagement.Application.SpeciesManagement.Queries.Get;

namespace PetFam.BreedManagement.Presentation.Requests;

public record GetSpeciesFilteredWithPaginationRequest(
    int? PositionFrom,
    int? PositionTo,
    string? SortBy,
    string? SortDirection,
    int Page,
    int PageSize)
{
    public GetSpeciesFilteredWithPaginationQuery ToQuery()
    {
        return new GetSpeciesFilteredWithPaginationQuery(
            PositionFrom,
            PositionTo,
            SortBy,
            SortDirection,
            Page,
            PageSize);
    }
};