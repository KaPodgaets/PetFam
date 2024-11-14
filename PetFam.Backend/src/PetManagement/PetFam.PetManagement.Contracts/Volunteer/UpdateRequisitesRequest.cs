namespace PetFam.PetManagement.Contracts.Volunteer
{
    public record UpdateRequisitesRequest(IEnumerable<RequisiteDto>? Requisites);
}
