using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Queries.GetPets
{
    public record GetFilteredPetsWithPaginationQuery(
        Guid? SpeciesId,
        int? PositionFrom,
        int? PositionTo,
        int Page,
        int PageSize) : IQuery;
}
