using PetFam.Application.FileProvider;

namespace PetFam.Api.Contracts
{
    public record FileDataDto(MemoryStream MemoryStream, FileMetedata FileMetadata);
}
