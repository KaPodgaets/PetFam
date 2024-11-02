using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer.Pet
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
                return Errors.General.ValueIsInvalid(nameof(FilePath)).ToErrorList();

            return new PetPhoto(filePath, isMain);
        }
    }
}
