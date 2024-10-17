using PetFam.Domain.Shared;

namespace PetFam.Application.FileProvider
{
    public interface IFileProvider
    {
        Task<Result<string>> UploadFile(FileData fileData, CancellationToken cancellationToken = default);
    }
}