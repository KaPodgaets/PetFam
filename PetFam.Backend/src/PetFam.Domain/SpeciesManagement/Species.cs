using PetFam.Domain.Shared;

namespace PetFam.Domain.SpeciesManagement
{
    public class Species : Entity<SpeciesId>, ISoftDeletable
    {
        private bool _isDeleted = false;
        // EF Core ctor
        private Species(SpeciesId id) : base(id) { }

        public string Name { get; private set; } = null!;
        public List<Breed> Breeds { get; private set; } = [];

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
