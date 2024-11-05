using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Queries.GetAllVolunteers
{
    public record GetVolunteersWithPaginationQuery(int PageNumber, int PageSize) : IQuery;
}
