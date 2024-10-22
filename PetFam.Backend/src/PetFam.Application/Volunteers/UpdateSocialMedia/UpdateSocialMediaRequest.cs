namespace PetFam.Application.Volunteers.UpdateSocialMedia
{
    public record UpdateSocialMediaRequest(
        Guid Id,
        UpdateSocialMediaDto Dto);

    public record UpdateSocialMediaDto(
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks);
}
