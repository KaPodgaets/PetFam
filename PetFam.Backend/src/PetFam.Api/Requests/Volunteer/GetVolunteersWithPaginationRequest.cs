using PetFam.Application.VolunteerManagement.Queries.GetAllVolunteers;

namespace PetFam.Api.Requests.Volunteer
{
    public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
    {
        public GetVolunteersWithPaginationQuery ToQuery()
        {
            return new GetVolunteersWithPaginationQuery(Page, PageSize);
        }
    }
}
