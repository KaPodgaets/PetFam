namespace PetFam.Application.Dtos.ValueObjects
{
    public class PetPhotoDto
    {
        public PetPhotoDto(string filepath)
        {
            Filepath = filepath;
        }
        public string Filepath { get; init; } = string.Empty;
    }
}
