namespace PetFam.Application.Volunteers.UpdateName
{
    public record UpdateMainInfoRequest(
        Guid Id,
        UpdateMainInfoDto Dto);

    public record UpdateMainInfoDto(
        FullNameDto FullNameDto);
}
