using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.UpdateRequisites
{
    public record UpdateRequisitesCommand(
        Guid Id,
        UpdateRequisitesDto Dto);

    public record UpdateRequisitesDto(
        IEnumerable<RequisiteDto>? Requisites);
}
