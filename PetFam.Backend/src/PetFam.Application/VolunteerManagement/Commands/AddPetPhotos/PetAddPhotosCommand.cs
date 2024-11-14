using PetFam.Application.FileProvider;
using PetFam.Shared.Abstructions;

namespace PetFam.Application.VolunteerManagement.Commands.AddPetPhotos
{
    public record PetAddPhotosCommand(
        Guid VolunteerId,
        Guid PetId,
        Content Content):ICommand;
}
