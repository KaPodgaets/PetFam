using PetFam.Application.VolunteerManagement.Queries.GetAllPets;

namespace PetFam.Api.Requests
{
    public record GetPetsWithPaginationRequest(int Page, int PageSize)
    {
        public GetPetsWithPaginationQuery ToQuery()
        {
            return new GetPetsWithPaginationQuery(Page, PageSize);
        }
    }
}
