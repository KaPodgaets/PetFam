using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Domain.SpeciesManagement
{
    public class Species : Entity<SpeciesId>, ISoftDeletable
    {
        private bool _isDeleted = false;
        private List<Breed> _breeds = [];
        // EF Core ctor
        private Species(SpeciesId id) : base(id) { }

        private Species(SpeciesId id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; } = null!;
        public IReadOnlyList<Breed> Breeds => _breeds;

        public static Result<Species> Create(SpeciesId id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Errors.General.ValueIsInvalid("name");

            if (id.Value == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(VolunteerId));

            return new Species(id, name);
        }

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
