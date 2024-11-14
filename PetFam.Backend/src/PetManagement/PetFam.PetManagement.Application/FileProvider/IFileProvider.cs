namespace PetFam.PetManagement.Application.FileProvider
{
    public interface IFileProvider
    {
        Task<Result<List<string>>> GetFiles(string bucketName);
        Task<Result> UploadFiles(
            Content content,
            CancellationToken cancellationToken = default);
        Task<Result> CreateBucket(string bucketName, CancellationToken cancellationToken = default);
        Task<Result<string>> GetDownloadLink(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
        Task<Result> DeleteFile(FileMetadata fileMetadata, CancellationToken cancellationToken = default);
    }
}