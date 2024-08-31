using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public record BreedInfo
    {
        //ef
        private BreedInfo()
        {

        }

        private BreedInfo(SpeciesId speciesId, BreedId breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public SpeciesId SpeciesId { get; } = null!;
        public BreedId BreedId { get; } = null!;

        public static Result<BreedInfo> Create(SpeciesId speciesId, BreedId breedId)
        {
            return new BreedInfo(speciesId, breedId);
        }

    }
}
