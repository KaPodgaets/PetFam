namespace PetFam.Domain.Volunteer
{
    public record VolunteerId
    {
        private VolunteerId()
        {
        }

        private VolunteerId(Guid id)
        {
            Value = id;
        }

        public Guid Value { get; }
        public static VolunteerId NewPetId() => new(Guid.NewGuid());
        public static VolunteerId Empty() => new(Guid.Empty);
        public static VolunteerId Create(Guid id) => new(id);
    }
}
