namespace PetFam.Volunteers.Contracts.Volunteer
{
    public record CreateVolunteerRequest(FullNameDto FullNameDto,
        string Email,
        IEnumerable<SocialMediaLinkDto>? SocialMediaLinks,
        IEnumerable<RequisiteDto>? Requisites)
    {
        public CreateVolunteerCommand ToCommand()
        {
            return new CreateVolunteerCommand(FullNameDto,
            Email,
            SocialMediaLinks,
            Requisites);
        }
    }
}
