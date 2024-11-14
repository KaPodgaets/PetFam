using PetFam.Application.Dtos.ValueObjects;

namespace PetFam.Api.Requests.Volunteer
{
    public record UpdateSocialMediaRequest(
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks);
}
