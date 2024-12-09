using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Commands;

public record ChangeApplicationStatusCommand(
    Guid Id):ICommand;