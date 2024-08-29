namespace PetFam.Domain.Models
{
    public class Volunteer
    {
        private readonly List<SocialMediaLink> _links = [];
        private readonly List<Pet> _pets = [];
        private readonly List<Requisite> _requisites = [];
        public Volunteer(FullName fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }

        public Guid Id { get; }
        public FullName FullName { get; }
        public string Email { get; } = string.Empty;
        public string GeneralInformation { get; } = string.Empty;
        public int AgesOfExpirience { get; }
        public IReadOnlyList<SocialMediaLink> Links => _links;
        public IReadOnlyList<Requisite> Requisites => _requisites;
        public IReadOnlyList<Pet> Pets => _pets;
        public int PetsFoundedHomeCount =>
            (Pets.Count(x => x.Status == PetStatus.Adopted));
        public int PetsLookingForHomeCount =>
            (Pets.Count(x => x.Status == PetStatus.LookingForHome));
        public int PetsOnTreatment =>
            (Pets.Count(x => x.Status == PetStatus.OnTreatment));
    }
}
