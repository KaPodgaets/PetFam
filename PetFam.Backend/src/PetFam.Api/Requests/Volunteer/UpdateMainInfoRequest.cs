using PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo;
using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Api.Requests.Volunteer
{
    public record UpdateMainInfoRequest(
        FullNameDto FullNameDto,
        int AgeOfExpirience,
        string Email,
        GeneralInformationDto GeneralInformationDto)
    {
        public UpdateMainInfoCommand ToCommand(Guid id)
        {
            return new UpdateMainInfoCommand(
                id,
                FullNameDto,
                AgeOfExpirience,
                Email,
                GeneralInformationDto);
        }
    }
}
