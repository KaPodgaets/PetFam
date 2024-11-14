namespace PetFam.Volunteers.Contracts.Volunteer
{
    public record UpdateSocialMediaRequest(
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks);
}
