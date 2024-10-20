using PetFam.Domain.Shared;

namespace PetFam.Application.FileProvider
{
    public interface IFileProvider
    {
        Task<Result<string>> UploadFile(FileData fileData, CancellationToken cancellationToken = default);
        Task<Result> CreateBucket(string bucketName, CancellationToken cancellationToken = default);
        Task<Result<string>> GetDownloadLink(FileMetedata fileMetedata, CancellationToken cancellationToken = default);
        Task<Result> DeleteFile(FileMetedata fileMetedata, CancellationToken cancellationToken = default);
    }
}