using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Commands.DeletePet;

public record DeletePetCommand(
    Guid VolunteerId,
    Guid PetId):ICommand;