using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Commands.Shared;

public record ChangeApplicationStatusCommand(
    Guid Id):ICommand;