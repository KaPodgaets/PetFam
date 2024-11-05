using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer.Pet
{
    public record PetPhoto
    {
        private PetPhoto(string filePath)
        {
            FilePath = filePath;
        }
        public string FilePath { get; }

        public static Result<PetPhoto> Create(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return Errors.General.ValueIsInvalid(nameof(FilePath)).ToErrorList();

            return new PetPhoto(filePath);
        }
    }
}
