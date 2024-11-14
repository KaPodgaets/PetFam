namespace PetFam.Shared.SharedKernel.ValueObjects.Pet
{
    public record PetId
    {
        private PetId()
        {
        }

        private PetId(Guid id)
        {
            Value = id;
        }

        public Guid Value { get; }
        public static PetId NewPetId() => new(Guid.NewGuid());
        public static PetId Empty() => new(Guid.Empty);
        public static PetId Create(Guid id) => new(id);
    }
}
