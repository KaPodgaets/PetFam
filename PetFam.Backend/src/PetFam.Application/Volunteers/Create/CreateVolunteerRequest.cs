namespace PetFam.Application.Volunteers.Create
{
    public record CreateVolunteerRequest(
        FullNameDto FullNameDto,
        string Email,
        List<SocialMediaLinkDto>? SocialMediaLinks,
        List<RequisiteDto>? Requisites);
}
