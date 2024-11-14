namespace PetFam.PetManagement.Contracts.Volunteer
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
