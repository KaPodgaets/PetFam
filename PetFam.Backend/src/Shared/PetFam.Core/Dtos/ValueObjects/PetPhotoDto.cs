namespace PetFam.Shared.Dtos.ValueObjects
{
    public class PetPhotoDto
    {
        public PetPhotoDto(string filepath, bool isMain)
        {
            Filepath = filepath;
            IsMain = isMain;
        }

        public bool IsMain { get; set; }
        public string Filepath { get; init; } = string.Empty;
    }
}