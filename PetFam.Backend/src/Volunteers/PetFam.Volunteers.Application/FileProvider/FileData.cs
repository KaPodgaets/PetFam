namespace PetFam.Volunteers.Application.FileProvider
{
    public record Content(
        IEnumerable<FileData> FilesData,
        string BucketName);

    public record FileData(
        Stream Stream,
        FileMetadata FileMetadata);

    public record FileMetadata(string BucketName, string ObjectName);
}
