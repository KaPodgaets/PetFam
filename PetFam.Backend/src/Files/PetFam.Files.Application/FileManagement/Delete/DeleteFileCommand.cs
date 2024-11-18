using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos;

namespace PetFam.Files.Application.FileManagement.Delete
{
    public record DeleteFileCommand(FileMetadata FileMetadata) : ICommand;
}
