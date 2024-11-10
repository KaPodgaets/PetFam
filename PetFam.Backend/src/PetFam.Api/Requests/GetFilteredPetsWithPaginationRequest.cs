using PetFam.Application.VolunteerManagement.Queries.GetPets;

namespace PetFam.Api.Requests
{
    public record GetFilteredPetsWithPaginationRequest(
        Guid? SpeciesId,
        int? PositionFrom,
        int? PositionTo,
        int Page,
        int PageSize)
    {
        public GetFilteredPetsWithPaginationQuery ToQuery()
        {
            return new GetFilteredPetsWithPaginationQuery(
                SpeciesId,
                PositionFrom,
                PositionTo,
                Page,
                PageSize);
        }
    }
}
