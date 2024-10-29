using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.UpdateSocialMedia
{
    public record UpdateSocialMediaRequest(
        Guid Id,
        UpdateSocialMediaDto Dto);

    public record UpdateSocialMediaDto(
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks);
}
