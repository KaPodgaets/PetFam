using PetFam.PetManagement.Application.VolunteerManagement.Queries.GetAllVolunteers;

namespace PetFam.PetManagement.Presentation.Requests
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
