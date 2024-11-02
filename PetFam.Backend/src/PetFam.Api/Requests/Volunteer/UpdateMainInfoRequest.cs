using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Api.Requests.Volunteer
{
    public record UpdateMainInfoRequest(
        FullNameDto FullNameDto,
        int AgeOfExpirience,
        string Email,
        GeneralInformationDto GeneralInformationDto);
}
