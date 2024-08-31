using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public record Gallery
    {
        private Gallery()
        {

        }

        private Gallery(List<PetPhoto> value)
        {
            Value = value;
        }

        public IReadOnlyList<PetPhoto> Value { get; }
        public int ImagesCount => Value.Count;

        public static Result<Gallery> Create(List<PetPhoto> petPhotos)
        {
            if (petPhotos.Count < Constants.MIN_ELEMENTS_IN_ARRAY)
                return "There is no photos in gallery";

            return new Gallery(petPhotos);
        }

    }
}
