namespace PetFam.Volunteers.Contracts.Volunteer
{
    public record UpdateRequisitesRequest(IEnumerable<RequisiteDto>? Requisites);
}
