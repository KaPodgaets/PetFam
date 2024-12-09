using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Commands.Update;

public record UpdateCommand(
    Guid Id,
    string VolunteerInfo) : ICommand;