using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.UpdateMainInfo
{
    public record UpdateMainInfoCommand(
        Guid Id,
        FullNameDto FullNameDto,
        int AgeOfExperience,
        string Email,
        GeneralInformationDto GeneralInformationDto):ICommand;
}
