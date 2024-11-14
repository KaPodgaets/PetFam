using PetFam.Application.Dtos.ValueObjects;

namespace PetFam.Api.Requests.Volunteer
{
    public record UpdateRequisitesRequest(IEnumerable<RequisiteDto>? Requisites);
}
