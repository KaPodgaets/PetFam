using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Commands.AssignAdmin;

public record AssignAdminCommand(
    Guid Id,
    Guid AdminId):ICommand;