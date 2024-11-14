using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.DeletePet;

public record DeletePetCommand(
    Guid VolunteerId,
    Guid PetId):ICommand;