using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateSocialMedia
{
    public record UpdateSocialMediaCommand(
        Guid Id,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks);
}
