using PetFam.Shared.Abstractions;

namespace PetFam.Application.VolunteerManagement.Commands.PetStatusUpdate;

public record PetStatusUpdateCommand(
    Guid VolunteerId,
    Guid PetId,
    int NewPetStatus):ICommand;