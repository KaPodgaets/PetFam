using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos;

namespace PetFam.Files.Application.FileManagement.GetLink
{
    public record GetFileLinkCommand(FileMetadata FileMetadata):ICommand;
}
