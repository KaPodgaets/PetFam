namespace PetFam.Domain.Models
{
    public record PetId(Guid Value)
    {
        public static PetId NewPetId() => new(Guid.NewGuid());
        public static PetId Empty() => new(Guid.Empty);
        public static PetId Create(Guid id) => new(id);
    }
}
