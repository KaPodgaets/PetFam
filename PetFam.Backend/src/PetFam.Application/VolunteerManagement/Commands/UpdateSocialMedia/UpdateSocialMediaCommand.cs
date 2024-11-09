using PetFam.Application.Dtos.ValueObjects;
using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateSocialMedia
{
    public record UpdateSocialMediaCommand(
        Guid Id,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks):ICommand;
}
