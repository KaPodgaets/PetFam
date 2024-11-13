using PetFam.Application.Interfaces;

namespace PetFam.Application.VolunteerManagement.Commands.ChangePetMainPhoto;

public record ChangePetMainPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string Path):ICommand;