using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Queries.GetAllPets
{
    public record GetPetsWithPaginationQuery
        (int PageNumber, int PageSize) : IQuery;
}
