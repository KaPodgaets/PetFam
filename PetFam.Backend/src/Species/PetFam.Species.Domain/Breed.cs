using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Abstractions;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.Species.Domain
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
