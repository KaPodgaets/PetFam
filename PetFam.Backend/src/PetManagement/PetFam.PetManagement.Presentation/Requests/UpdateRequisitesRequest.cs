using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.PetManagement.Presentation.Requests
{
    public record UpdateRequisitesRequest(IEnumerable<RequisiteDto>? Requisites);
}
