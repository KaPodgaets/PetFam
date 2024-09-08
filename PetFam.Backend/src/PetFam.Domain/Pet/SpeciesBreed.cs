using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
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
                return "Id of species can not be empty";

            if (breedId == Guid.Empty)
                return "Id of breed can not be empty";

            return new SpeciesBreed(speciesId, breedId);
        }
    }
}
