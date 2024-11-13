using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Commands.DeletePetPhotos;

public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<string> Paths):ICommand;