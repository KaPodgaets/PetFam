using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.DeletePetPhotos;

public record DeletePetPhotosCommand(
    Guid VolunteerId,
    Guid PetId,
    IEnumerable<string> Paths):ICommand;