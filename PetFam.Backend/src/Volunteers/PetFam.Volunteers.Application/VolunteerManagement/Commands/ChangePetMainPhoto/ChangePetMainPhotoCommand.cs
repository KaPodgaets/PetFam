namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.ChangePetMainPhoto;

public record ChangePetMainPhotoCommand(
    Guid VolunteerId,
    Guid PetId,
    string Path):ICommand;