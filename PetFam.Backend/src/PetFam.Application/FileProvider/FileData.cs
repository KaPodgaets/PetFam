namespace PetFam.Application.FileProvider
{
    public record Content(
        IEnumerable<FileData> FilesData,
        string BucketName);

    public record FileData(
        Stream Stream,
        FileMetedata FileMetadata);

    public record FileMetedata(string BucketName, string ObjectName);
}
