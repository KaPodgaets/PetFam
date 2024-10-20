using PetFam.Domain.Shared;

namespace PetFam.Domain.SpeciesManagement
{
    public class Breed : Entity<BreedId>, ISoftDeletable
    {
        private bool _isDeleted = true;
        // EF Core ctor
        private Breed(BreedId id) : base(id) { }

        public string Name { get; private set; } = null!;
        public void Delete()
        {
            _isDeleted = true;
        }
        public void Restore()
        {
            _isDeleted = false;
        }
    }
}
