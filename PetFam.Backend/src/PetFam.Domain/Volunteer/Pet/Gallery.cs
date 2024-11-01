﻿using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer.Pet
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

        public IReadOnlyList<PetPhoto> Value { get; } = [];
        public int ImagesCount => Value.Count;

        public static Result<Gallery> Create(IEnumerable<PetPhoto> petPhotos)
        {
            if (petPhotos.ToList().Count < Constants.MIN_ELEMENTS_IN_ARRAY)
                return Errors.General.ValueIsRequired(nameof(Gallery));

            return new Gallery(petPhotos);
        }
        public static Gallery CreateEmpty()
        {
            return new Gallery();
        }
    }
}
