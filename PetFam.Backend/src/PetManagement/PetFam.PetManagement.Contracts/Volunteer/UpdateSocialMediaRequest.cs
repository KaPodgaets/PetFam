namespace PetFam.PetManagement.Contracts.Volunteer
{
    public record UpdateSocialMediaRequest(
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks);
}
