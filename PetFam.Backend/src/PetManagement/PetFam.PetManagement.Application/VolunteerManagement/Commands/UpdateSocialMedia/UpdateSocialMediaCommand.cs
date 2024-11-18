using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.UpdateSocialMedia
{
    public record UpdateSocialMediaCommand(
        Guid Id,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks):ICommand;
}
