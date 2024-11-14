using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Commands.PetStatusUpdate;

public record PetStatusUpdateCommand(
    Guid VolunteerId,
    Guid PetId,
    int NewPetStatus):ICommand;