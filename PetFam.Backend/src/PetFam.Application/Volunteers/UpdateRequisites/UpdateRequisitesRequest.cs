namespace PetFam.Application.Volunteers.UpdateRequisites
{
    public record UpdateRequisitesRequest(
        Guid Id,
        UpdateRequisitesDto Dto);

    public record UpdateRequisitesDto(
        IEnumerable<RequisiteDto>? Requisites);
}
