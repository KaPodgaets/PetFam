namespace PetFam.Volunteers.Application.VolunteerManagement.Queries.GetAllVolunteers
{
    public record GetVolunteersWithPaginationQuery(
        Guid? VolunteerId,
        int PageNumber,
        int PageSize) : IQuery;
}
