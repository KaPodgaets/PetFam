namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.DeletePet;

public record DeletePetCommand(
    Guid VolunteerId,
    Guid PetId):ICommand;