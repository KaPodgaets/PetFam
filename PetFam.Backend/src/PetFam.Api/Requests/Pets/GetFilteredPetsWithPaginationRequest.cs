using PetFam.Application.VolunteerManagement.Queries.GetPets;

namespace PetFam.Api.Requests.Pets
{
    public record GetFilteredPetsWithPaginationRequest(
        Guid? VolunteerId,
        Guid? SpeciesId,
        Guid? BreedId,
        string? Nickname,
        int? AgeFrom,
        int? AgeTo,
        string? Color,
        string? Country,        
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
                VolunteerId,
                SpeciesId,
                BreedId,
                Nickname,
                AgeFrom,
                AgeTo,
                Color,
                Country,
                PositionFrom,
                PositionTo,
                SortBy,
                SortDirection,
                Page,
                PageSize);
        }
    }
}
