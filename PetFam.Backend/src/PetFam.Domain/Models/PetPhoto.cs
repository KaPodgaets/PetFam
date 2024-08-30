namespace PetFam.Domain.Models
{
    public class PetPhoto
    {
        public PetPhoto(string path, bool isMain)
        {
            if (string.IsNullOrEmpty(path)) throw new ArgumentNullException("FilePath can not be empty or null");

            FilePath = path;
            IsMain = isMain;
        }
        public string FilePath { get; set; } = string.Empty;
        public bool IsMain { get; set; }
    }
}
