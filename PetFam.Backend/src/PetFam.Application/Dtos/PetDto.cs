namespace PetFam.Application.Dtos
{
    public class PetDto
    {
        public Guid Id { get; init; }
        public Guid VolunteerId { get; init; }
        public string NickName { get; init; } = string.Empty;
        public Guid[] SpeciesAndBreed { get; init; } = [];
        public int Status { get; init; }
        public DateTime CreateDate { get; init; }
        public int Order { get; init; }
        public string[] Photos { get; init; } = [];
    }
}
