using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos;

namespace PetFam.Files.Application.FileManagement.Upload
{
    public record UploadFileCommand(Content Content) : ICommand;
}
