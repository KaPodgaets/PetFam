using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public record SpeciesAndBreed
    {
        public Guid SpeciesId { get; }
        public Guid BreedId { get; }
        private SpeciesAndBreed(Guid speciesId, Guid breedId)
        {
            SpeciesId = speciesId;
            BreedId = breedId;
        }

        public static Result<SpeciesAndBreed> Create(Guid speciesId, Guid breedId)
        {
            if (speciesId == Guid.Empty)
                return "Id of species can not be empty";

            if (breedId == Guid.Empty)
                return "Id of breed can not be empty";

            return new SpeciesAndBreed(speciesId, breedId);
        }
    }
}
