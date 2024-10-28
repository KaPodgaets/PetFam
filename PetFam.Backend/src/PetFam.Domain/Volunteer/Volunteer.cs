using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Domain.Volunteer
{
    public class Volunteer : Entity<VolunteerId>, ISoftDeletable
    {
        private readonly List<Pet.Pet> _pets = [];
        private bool _isDeleted = false;
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
            SocialMediaDetails = socialMediaDetails;
            Requisites = requisitesDetails;
        }
        public FullName FullName { get; private set; } = null!;
        public Email Email { get; private set; }
        public GeneralInformation? GeneralInformation1 { get; private set; }
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

        public void UpdateMainInfo(
            FullName fullName,
            Email email,
            int ageOfExpirience,
            GeneralInformation generalInformation)
        {
            FullName = fullName;
            Email = email;
            AgesOfExpirience = ageOfExpirience;
            GeneralInformation1 = generalInformation;
        }

        public void UpdateSocialMedia(
            SocialMediaDetails socialMediaDetails)
        {
            SocialMediaDetails = socialMediaDetails;
        }

        public void UpdateRequisite(
            RequisitesDetails requisites)
        {
            Requisites = requisites;
        }

        public void Delete()
        {
            foreach(var pet in _pets) 
            {
                pet.Delete(); 
            }
            _isDeleted = true;
        }
        public void Restore()
        {
            foreach (var pet in _pets)
            {
                pet.Restore();
            }
            _isDeleted = false;
        }

        public void AddPet(Pet.Pet pet)
        {
            pet.ChangeOrderNumber(_pets.Count);
            _pets.Add(pet);
        }

        public void SortPets()
        {
            int orderNumber = 1;
            foreach(var pet in _pets)
            {
                pet.ChangeOrderNumber(orderNumber);
                orderNumber++;
            }
        }

        public Result ChangePetOrder(Pet.Pet pet, int newOrderNumber)
        {
            if (newOrderNumber == pet.Order)
                return Result.Success();

            var increment = pet.Order < newOrderNumber ? -1 : 1;
            var start = int.Min(newOrderNumber, pet.Order);
            var end = int.Max(newOrderNumber, pet.Order);

            foreach(var petFromList in _pets)
            {
                if (petFromList.Order >= end && petFromList.Order <= start)
                {
                    if (petFromList.Id.Value == pet.Id.Value)
                    {
                        pet.ChangeOrderNumber(newOrderNumber);
                    }
                    else
                    {
                        petFromList.ChangeOrderNumber(petFromList.Order + increment);
                    }
                }
            }

            return Result.Success();
        }
    }
}
