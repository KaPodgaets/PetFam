using PetFam.Domain.Shared;

namespace PetFam.Domain.SpeciesManagement
{
    public class Breed : Entity<BreedId>, ISoftDeletable
    {
        private bool _isDeleted = true;
        // EF Core ctor
        public Breed(BreedId id) : base(id) { }
        private Breed(BreedId id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; } = null!;

        public static Result<Breed> Create(BreedId id, string name)
        {
            if(string.IsNullOrEmpty(name))
                return Errors.General.ValueIsRequired(nameof(name)).ToErrorList();

            return new Breed(id, name);
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
