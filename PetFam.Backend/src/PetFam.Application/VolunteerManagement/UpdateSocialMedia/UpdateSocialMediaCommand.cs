using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.UpdateSocialMedia
{
    public record UpdateSocialMediaCommand(
        Guid Id,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks);
}
