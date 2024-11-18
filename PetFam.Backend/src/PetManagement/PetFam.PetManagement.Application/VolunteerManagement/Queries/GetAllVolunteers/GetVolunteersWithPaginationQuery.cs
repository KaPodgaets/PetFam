using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Application.VolunteerManagement.Queries.GetAllVolunteers
{
    public record GetVolunteersWithPaginationQuery(
        Guid? VolunteerId,
        int PageNumber,
        int PageSize) : IQuery;
}
