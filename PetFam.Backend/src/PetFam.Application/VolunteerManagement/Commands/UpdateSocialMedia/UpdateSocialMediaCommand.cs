using PetFam.Application.Dtos.ValueObjects;
using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateSocialMedia
{
    public record UpdateSocialMediaCommand(
        Guid Id,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks):ICommand;
}
