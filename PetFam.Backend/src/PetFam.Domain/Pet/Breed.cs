using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{

    public class Breed : Entity<BreedId>
    {
        //ef
        private Breed(BreedId id) : base(id)
        {

        }

        private Breed(BreedId id,
            string name,
            SpeciesId speciesId) : base(id)
        {
            Name = name;
            SpeciesId = speciesId;
        }

        public string Name { get; private set; } = string.Empty;
        public SpeciesId SpeciesId { get; private set; } = null!;

        public static Result<Breed> Create(BreedId id, string name, SpeciesId speciesId)
        {
            if (id == null)
                return "id could not be null";
            if (string.IsNullOrWhiteSpace(name))
                return "Breed's name can not be empty";

            return new Breed(id, name, speciesId);
        }
    }
}
