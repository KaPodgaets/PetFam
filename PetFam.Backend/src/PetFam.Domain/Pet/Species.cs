using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public class Species : Entity<SpeciesId>
    {
        //ef
        private Species(SpeciesId id) : base(id)
        {

        }

        private Species(SpeciesId id, string name) : base(id)
        {
            Name = name;
        }
        public string Name { get; private set; } = string.Empty;
        public IReadOnlyList<Breed> Breeds { get; private set; } = [];

        public static Result<Species> Create(SpeciesId id, string name)
        {
            if (id == null)
                return "id could not be null";
            if (string.IsNullOrWhiteSpace(name))
                return "Name of species can not be empty";

            return new Species(id, name);
        }
    }
}
