using PetFam.Domain.Pet;
using PetFam.Domain.Shared;

namespace PetFam.Domain.Volunteer
{
    public class Volunteer : Entity<VolunteerId>
    {
        private readonly List<Pet.Pet> _pets = [];

        private Volunteer(VolunteerId id) : base(id)
        {
        }
        public Volunteer(VolunteerId id,
            FullName fullName,
            string email)
            : base(id)
        {
            FullName = fullName;
            Email = email;
        }
        public FullName FullName { get; private set; }
        public string Email { get; private set; } = string.Empty;
        public string GeneralInformation { get; private set; } = string.Empty;
        public int AgesOfExpirience { get; private set; }
        public SocialMediaDetails? SocialMediaDetails { get; private set; }
        public RequisitesDetails? Requisites { get; private set; }
        public IReadOnlyList<Pet.Pet> Pets => _pets;
        public int PetsFoundedHomeCount =>
            _pets.Count(x => x.Status == PetStatus.Adopted);
        public int PetsLookingForHomeCount =>
            _pets.Count(x => x.Status == PetStatus.LookingForHome);
        public int PetsOnTreatment =>
            _pets.Count(x => x.Status == PetStatus.OnTreatment);
    }
}
