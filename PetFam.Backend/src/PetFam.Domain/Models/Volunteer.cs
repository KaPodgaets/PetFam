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

        public Guid Id { get; private set; }
        public FullName FullName { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string GeneralInformation { get; private set; } = string.Empty;
        public int AgesOfExpirience { get; private set; }
        public IReadOnlyList<SocialMediaLink> Links => _links;
        public IReadOnlyList<Requisite> Requisites => _requisites;
        public IReadOnlyList<Pet> Pets => _pets;
        public int PetsFoundedHomeCount =>
            (_pets.Count(x => x.Status == PetStatus.Adopted));
        public int PetsLookingForHomeCount =>
            (_pets.Count(x => x.Status == PetStatus.LookingForHome));
        public int PetsOnTreatment =>
            (_pets.Count(x => x.Status == PetStatus.OnTreatment));
    }
}
