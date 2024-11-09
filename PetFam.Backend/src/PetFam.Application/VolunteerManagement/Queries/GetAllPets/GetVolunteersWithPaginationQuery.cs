using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Queries.GetAllPets
{
    public record GetFilteredPetsWithPaginationQuery(
        Guid? SpeciesId,
        int? PositionFrom,
        int? PositionTo,
        int Page,
        int PageSize) : IQuery;
}
