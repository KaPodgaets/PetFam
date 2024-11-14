namespace PetFam.Shared.ValueObjects.Pet
{
    public record Gallery
    {
        private readonly List<PetPhoto> _value = [];
        private Gallery()
        {
        }

        private Gallery(IEnumerable<PetPhoto> value)
        {
            _value = value.ToList();
        }

        public IReadOnlyList<PetPhoto> Value => _value;
        public int ImagesCount => Value.Count;

        public static Result<Gallery> Create(IEnumerable<PetPhoto> petPhotos)
        {
            if (petPhotos.ToList().Count<Constants.MIN_ELEMENTS_IN_ARRAY)
                return Errors.General.ValueIsRequired(nameof(Gallery)).ToErrorList();

            return new Gallery(petPhotos);
        }
        public static Gallery CreateEmpty()
        {
            return new Gallery();
        }
    }
}
