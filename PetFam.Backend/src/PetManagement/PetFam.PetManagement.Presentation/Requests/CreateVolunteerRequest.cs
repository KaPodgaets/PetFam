using PetFam.PetManagement.Application.VolunteerManagement.Commands.Create;
using PetFam.Shared.Dtos.ValueObjects;

namespace PetFam.PetManagement.Presentation.Requests
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
