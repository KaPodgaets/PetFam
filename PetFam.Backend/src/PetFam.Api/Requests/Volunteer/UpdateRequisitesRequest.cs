using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Api.Requests.Volunteer
{
    public record UpdateRequisitesRequest(IEnumerable<RequisiteDto>? Requisites);
}
