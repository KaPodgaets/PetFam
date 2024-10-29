using PetFam.Application.FileProvider;

namespace PetFam.Application.VolunteerManagement.PetManagement.AddPhotos
{
    public record PetAddPhotosCommand(
        Guid VolunteerId,
        Guid PetId,
        Content Content);
}
