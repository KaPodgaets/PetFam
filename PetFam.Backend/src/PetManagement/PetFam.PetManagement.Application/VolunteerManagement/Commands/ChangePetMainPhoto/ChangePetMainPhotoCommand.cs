using PetFam.Shared.Abstractions;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.ChangePetMainPhoto;

public record ChangePetMainPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string Path):ICommand;