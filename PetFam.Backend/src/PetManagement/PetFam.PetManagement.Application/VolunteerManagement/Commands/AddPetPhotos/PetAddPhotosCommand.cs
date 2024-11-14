using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.AddPetPhotos
{
    public record PetAddPhotosCommand(
        Guid VolunteerId,
        Guid PetId,
        Content Content):ICommand;
}
