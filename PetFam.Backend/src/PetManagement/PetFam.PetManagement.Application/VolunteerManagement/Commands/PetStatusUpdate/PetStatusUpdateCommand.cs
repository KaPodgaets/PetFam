using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.PetStatusUpdate;

public record PetStatusUpdateCommand(
    Guid VolunteerId,
    Guid PetId,
    int NewPetStatus):ICommand;