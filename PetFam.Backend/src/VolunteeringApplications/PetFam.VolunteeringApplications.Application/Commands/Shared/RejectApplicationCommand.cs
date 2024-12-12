using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Commands.Shared;

public record RejectApplicationCommand(
    Guid Id,
    string Comment):ICommand;