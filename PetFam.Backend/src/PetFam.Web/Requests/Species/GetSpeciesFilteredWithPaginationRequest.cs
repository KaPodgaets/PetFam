using PetFam.Application.SpeciesManagement.Queries.Get;

namespace PetFam.Api.Requests.Species;

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