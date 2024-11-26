using PetFam.Shared.SharedKernel.Abstractions;

namespace PetFam.Shared.SharedKernel
{
    public abstract class Entity<TId>: SoftDeletableEntity where TId : notnull
    {
        protected Entity(TId id)
        {
            Id = id;
        }

        public TId Id { get; private set; }
    }
}
