namespace PetFam.Shared.Dtos
{
    public record Content(
        IEnumerable<FileDataDto> FilesData,
        string BucketName);

    public record FileDataDto(
        Stream Stream,
        FileMetadata FileMetadata);

    public record FileMetadata(string BucketName, string ObjectName);
}
