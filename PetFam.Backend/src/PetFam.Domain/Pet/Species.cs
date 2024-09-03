using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public class Species : Entity<SpeciesId>
    {
        // EF Core ctor
        private Species(SpeciesId id) : base(id) { }

        public string Name { get; private set; } = null!;
        public List<Breed> Breeds { get; private set; } = [];
    }
}
