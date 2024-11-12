using PetFam.Application.VolunteerManagement.Queries.GetPets;

namespace PetFam.Api.Requests.Pets
{
    public record GetFilteredPetsWithPaginationRequest(
        Guid? SpeciesId,
        int? PositionFrom,
        int? PositionTo,
        string? SortBy,
        string? SortDirection,
        int Page,
        int PageSize)
    {
        public GetFilteredPetsWithPaginationQuery ToQuery()
        {
            return new GetFilteredPetsWithPaginationQuery(
                SpeciesId,
                PositionFrom,
                PositionTo,
                SortBy,
                SortDirection,
                Page,
                PageSize);
        }
    }
}
