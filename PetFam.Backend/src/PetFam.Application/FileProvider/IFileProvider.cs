using PetFam.Domain.Shared;

namespace PetFam.Application.FileProvider
{
    public interface IFileProvider
    {
        Task<Result<List<string>>> GetFiles(string bucketName);
        Task<Result> UploadFiles(
            Content content,
            CancellationToken cancellationToken = default);
        Task<Result> CreateBucket(string bucketName, CancellationToken cancellationToken = default);
        Task<Result<string>> GetDownloadLink(FileMetedata fileMetedata, CancellationToken cancellationToken = default);
        Task<Result> DeleteFile(FileMetedata fileMetedata, CancellationToken cancellationToken = default);
    }
}