namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid Id,
        FullNameDto FullNameDto,
        int AgeOfExperience,
        string Email,
        GeneralInformationDto GeneralInformationDto):ICommand;
}
