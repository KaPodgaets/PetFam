namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.Create
{
    public record CreateVolunteerCommand(
        FullNameDto FullNameDto,
        string Email,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks,
        IEnumerable<RequisiteDto>? Requisites) :ICommand;
}
