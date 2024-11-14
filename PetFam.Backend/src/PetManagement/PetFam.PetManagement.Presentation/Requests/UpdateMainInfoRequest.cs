using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.PetManagement.Presentation.Requests
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
