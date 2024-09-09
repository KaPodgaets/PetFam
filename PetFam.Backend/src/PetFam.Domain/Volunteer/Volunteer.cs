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
        private Volunteer(VolunteerId id,
            FullName fullName,
            Email email,
            SocialMediaDetails? socialMediaDetails,
            RequisitesDetails? requisitesDetails)
            : base(id)
        {
            FullName = fullName;
            Email = email;
        }
        public FullName FullName { get; private set; } = null!;
        public Email Email { get; private set; }
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

        public static Result<Volunteer> Create(
            VolunteerId id,
            FullName fullName,
            Email email,
            SocialMediaDetails? socialMediaDetails,
            RequisitesDetails? requisitesDetails)
        {
            if (string.IsNullOrWhiteSpace(email.Value))
                return Errors.General.ValueIsInvalid("email");

            if (id.Value == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(VolunteerId));

            return new Volunteer(id, fullName, email, socialMediaDetails, requisitesDetails);
        }
    }
}
