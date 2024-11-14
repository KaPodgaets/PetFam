using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Commands.ChangePetMainPhoto;

public record ChangePetMainPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string Path):ICommand;