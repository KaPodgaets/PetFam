using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public record Gallery
    {
        private Gallery()
        {

        }

        private Gallery(IEnumerable<PetPhoto> value)
        {
            Value = value.ToList();
        }

        public IReadOnlyList<PetPhoto> Value { get; } = null!;
        public int ImagesCount => Value.Count;

        public static Result<Gallery> Create(IEnumerable<PetPhoto> petPhotos)
        {
            if (petPhotos.ToList().Count < Constants.MIN_ELEMENTS_IN_ARRAY)
                return "There is no photos in gallery";

            return new Gallery(petPhotos);
        }

    }
}
