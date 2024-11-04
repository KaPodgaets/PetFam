using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Api.Requests.Volunteer
{
    public record UpdateSocialMediaRequest(
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks);
}
