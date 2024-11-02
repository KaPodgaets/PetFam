namespace PetFam.Application.FileManagement
{
    public interface IFilesCleanerMessageQueue
    {
        Task<string[]> ReadAsync(CancellationToken cancellationToken);
        Task WriteAsync(string[] paths, CancellationToken cancellationToken);
    }
}