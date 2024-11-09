using PetFam.Application.Dtos.ValueObjects;
using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid Id,
        FullNameDto FullNameDto,
        int AgeOfExperience,
        string Email,
        GeneralInformationDto GeneralInformationDto):ICommand;
}
