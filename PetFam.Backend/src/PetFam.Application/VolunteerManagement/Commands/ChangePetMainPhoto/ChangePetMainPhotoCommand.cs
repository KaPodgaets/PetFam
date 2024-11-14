using PetFam.Shared.Abstractions;

namespace PetFam.Application.VolunteerManagement.Commands.ChangePetMainPhoto;

public record ChangePetMainPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string Path):ICommand;