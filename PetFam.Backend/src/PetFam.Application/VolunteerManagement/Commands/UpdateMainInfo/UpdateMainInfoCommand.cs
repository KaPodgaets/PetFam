using PetFam.Application.Interfaces;
using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid Id,
        FullNameDto FullNameDto,
        int AgeOfExperience,
        string Email,
        GeneralInformationDto GeneralInformationDto):ICommand;
}
