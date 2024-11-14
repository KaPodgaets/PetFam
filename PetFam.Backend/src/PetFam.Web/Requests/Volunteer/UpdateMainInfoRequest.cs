using PetFam.Application.Dtos.ValueObjects;
using PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo;

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
