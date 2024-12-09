using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Commands.CreateApplication;

public record CreateCommand(
    Guid UserId,
    string VolunteerInfo):ICommand;