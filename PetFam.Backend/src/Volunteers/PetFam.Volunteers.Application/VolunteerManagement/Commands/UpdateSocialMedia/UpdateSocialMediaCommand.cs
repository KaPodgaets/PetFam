namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.UpdateSocialMedia
{
    public record UpdateSocialMediaCommand(
        Guid Id,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks):ICommand;
}
