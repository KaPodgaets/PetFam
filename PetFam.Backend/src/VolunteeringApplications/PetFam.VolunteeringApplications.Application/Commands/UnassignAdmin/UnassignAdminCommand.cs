using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Commands.UnassignAdmin;

public record UnassignAdminCommand(
    Guid Id,
    Guid AdminId):ICommand;