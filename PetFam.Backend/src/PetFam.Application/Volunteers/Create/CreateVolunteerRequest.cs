namespace PetFam.Application.Volunteers.Create
{
    public record CreateVolunteerRequest(
        FullNameDto FullNameDto,
        string Email,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks,
        IEnumerable<RequisiteDto>? Requisites);
}
