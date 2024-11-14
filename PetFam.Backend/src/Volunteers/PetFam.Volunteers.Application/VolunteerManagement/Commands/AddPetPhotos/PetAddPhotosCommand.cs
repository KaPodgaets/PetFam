using PetFam.Volunteers.Application.FileProvider;

namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.AddPetPhotos
{
    public record PetAddPhotosCommand(
        Guid VolunteerId,
        Guid PetId,
        Content Content):ICommand;
}
