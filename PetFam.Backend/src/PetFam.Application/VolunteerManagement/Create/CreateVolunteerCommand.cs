using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Application.VolunteerManagement.Create
{
    public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Email,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks,
        IEnumerable<RequisiteDto>? Requisites);
}
