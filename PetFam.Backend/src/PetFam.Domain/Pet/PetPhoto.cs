using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public record PetPhoto
    {
        private PetPhoto(string filePath, bool isMain)
        {
            FilePath = filePath;
            IsMain = isMain;
        }
        public string FilePath { get; }
        public bool IsMain { get; }

        public static Result<PetPhoto> Create(string filePath, bool isMain)
        {
            if (string.IsNullOrEmpty(filePath))
                return "FilePath can not be empty or null";

            return new PetPhoto(filePath, isMain);
        }
    }
}
