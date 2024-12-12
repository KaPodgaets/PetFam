using PetFam.VolunteeringApplications.Application.Commands.CreateApplication;

namespace PetFam.VolunteeringApplications.Presentation;

public record CreateApplicationRequest(Guid UserId, string VolunteerInfo)
{
    public CreateCommand ToCommand()
    {
        return new CreateCommand(UserId, VolunteerInfo);
    }
}