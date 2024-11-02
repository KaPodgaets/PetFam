using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid Id,
        UpdateMainInfoDto Dto);

    public record UpdateMainInfoDto(
        FullNameDto FullNameDto,
        int AgeOfExpirience,
        string Email,
        GeneralInformationDto GeneralInformationDto);
}
