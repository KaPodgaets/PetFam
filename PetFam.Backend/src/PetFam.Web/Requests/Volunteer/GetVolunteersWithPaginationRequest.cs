using PetFam.Application.VolunteerManagement.Queries.GetAllVolunteers;

namespace PetFam.Api.Requests.Volunteer
{
    public record GetVolunteersWithPaginationRequest(
        Guid? VolunteerId,
        int Page,
        int PageSize)
    {
        public GetVolunteersWithPaginationQuery ToQuery()
        {
            return new GetVolunteersWithPaginationQuery(VolunteerId, Page, PageSize);
        }
    }
}
