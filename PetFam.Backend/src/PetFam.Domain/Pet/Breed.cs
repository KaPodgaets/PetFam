using PetFam.Domain.Shared;

namespace PetFam.Domain.Pet
{
    public class Breed : Entity<BreedId>
    {
        // EF Core ctor
        private Breed(BreedId id) : base(id) { }

        public string Name { get; private set; } = null!;
    }
}
