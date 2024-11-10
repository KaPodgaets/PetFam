using PetFam.Application.Dtos.ValueObjects;
using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Commands.Create
{
    public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Email,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks,
        IEnumerable<RequisiteDto>? Requisites) :ICommand;
}
