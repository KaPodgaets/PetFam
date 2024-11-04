using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid Id,
        FullNameDto FullNameDto,
        int AgeOfExperience,
        string Email,
        GeneralInformationDto GeneralInformationDto);
}
