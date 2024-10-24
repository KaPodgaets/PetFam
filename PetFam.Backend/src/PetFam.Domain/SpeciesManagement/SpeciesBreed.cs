using PetFam.Domain.Shared;

namespace PetFam.Domain.SpeciesManagement
{
    public record SpeciesBreed
    {
        public SpeciesId SpeciesId { get; }
        public Guid BreedId { get; }
        private SpeciesBreed(SpeciesId speciesId, Guid breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public static Result<SpeciesBreed> Create(SpeciesId speciesId, Guid breedId)
        {
            if (speciesId.Value == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(SpeciesId));

            if (breedId == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(BreedId));

            return new SpeciesBreed(speciesId, breedId);
        }
    }
}
