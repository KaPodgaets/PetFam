namespace PetFam.Domain.Pet
{
    public record BreedId
    {
        private BreedId()
        {
        }

        private BreedId(Guid id)
        {
            Value = id;
        }

        public Guid Value { get; }
        public static BreedId NewPetId() => new(Guid.NewGuid());
        public static BreedId Empty() => new(Guid.Empty);
        public static BreedId Create(Guid id) => new(id);
    }
}
