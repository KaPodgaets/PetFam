using PetFam.Application.VolunteerManagement.Queries;

namespace PetFam.Api.Requests
{
    public record GetVolunteersWithPaginationRequest(int Page, int PageSize)
    {
        public GetVolunteersWithPaginationQuery ToQuery()
        {
            return new GetVolunteersWithPaginationQuery(Page, PageSize);
        }
    }
}
