using PetFam.Application.VolunteerManagement.Create;
using PetFam.Application.VolunteerManagement.ValueObjects;

namespace PetFam.Api.Requests.Volunteer
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
