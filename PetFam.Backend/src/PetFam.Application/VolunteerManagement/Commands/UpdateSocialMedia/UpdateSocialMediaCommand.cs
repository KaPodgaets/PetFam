using PetFam.Application.Dtos.ValueObjects;
using PetFam.Shared.Abstractions;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateSocialMedia
{
    public record UpdateSocialMediaCommand(
        Guid Id,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks):ICommand;
}
