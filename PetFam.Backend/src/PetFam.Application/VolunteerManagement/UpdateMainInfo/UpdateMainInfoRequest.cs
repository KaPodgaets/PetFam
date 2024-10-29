using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.UpdateMainInfo
{
    public record UpdateMainInfoRequest(
        Guid Id,
        UpdateMainInfoDto Dto);

    public record UpdateMainInfoDto(
        FullNameDto FullNameDto,
        int AgeOfExpirience,
        string Email,
        GeneralInformationDto GeneralInformationDto);
}
