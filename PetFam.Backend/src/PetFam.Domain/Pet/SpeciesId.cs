namespace PetFam.Domain.Pet
{
    public record SpeciesId
    {
        private SpeciesId()
        {
        }

        private SpeciesId(Guid id)
        {
            Value = id;
        }

        public Guid Value { get; }
        public static SpeciesId NewPetId() => new(Guid.NewGuid());
        public static SpeciesId Empty() => new(Guid.Empty);
        public static SpeciesId Create(Guid id) => new(id);
    }
}
