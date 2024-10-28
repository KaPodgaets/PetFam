namespace PetFam.Application.FileProvider
{
    public record FileData(
        Stream Stream,
        FileMetedata FileMetadata);

    public record FileMetedata(string BucketName, string ObjectName);
}
