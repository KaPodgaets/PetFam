using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Queries.GetAllVolunteers
{
    public record GetVolunteersWithPaginationQuery(
        Guid? VolunteerId,
        int PageNumber,
        int PageSize) : IQuery;
}
