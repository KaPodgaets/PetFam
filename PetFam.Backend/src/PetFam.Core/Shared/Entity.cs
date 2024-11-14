using System.Security.Cryptography.X509Certificates;

namespace PetFam.Domain.Shared
{
    public abstract class Entity<TId> where TId : notnull
    {
        protected Entity(TId id)
        {
            Id = id;
        }

        public TId Id { get; private set; }
    }
}
