using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.PetManagement.Presentation.Requests
{
    public record UpdateSocialMediaRequest(
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks);
}
