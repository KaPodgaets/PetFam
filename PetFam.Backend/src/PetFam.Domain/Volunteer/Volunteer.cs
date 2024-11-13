using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Domain.Volunteer
{
    public class Volunteer : Entity<VolunteerId>, ISoftDeletable
    {
        private List<Pet.Pet> _pets = [];
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
        public int AgesOfExperience { get; private set; }
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
                return Errors.General.ValueIsInvalid("email").ToErrorList();

            if (id.Value == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(VolunteerId)).ToErrorList();

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
            AgesOfExperience = ageOfExpirience;
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
            foreach (var pet in _pets)
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

        public void DeletePet(Pet.Pet pet)
        {
            pet.Delete();
        }

        public void SortPets()
        {
            int orderNumber = 1;
            foreach (var pet in _pets)
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

            foreach (var petFromList in _pets)
            {
                if (petFromList.Order >= start && petFromList.Order <= end)
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

        public Result SetPetFirstInOrder(Pet.Pet pet)
        {
            return ChangePetOrder(pet, 0);
        }

        public Result SetPetLastInOrder(Pet.Pet pet)
        {
            return ChangePetOrder(pet, _pets.Count - 1);
        }
        /// <summary>
        /// Update pet info expect Order in pet list and Photos
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nickName"></param>
        /// <param name="speciesAndBreed"></param>
        /// <param name="status"></param>
        /// <param name="generalInfo"></param>
        /// <param name="healthInfo"></param>
        /// <param name="address"></param>
        /// <param name="accountInfo"></param>
        /// <returns></returns>
        public Result UpdatePet(
            PetId id,
            string nickName,
            SpeciesBreed speciesAndBreed,
            PetStatus status,
            PetGeneralInfo generalInfo,
            PetHealthInfo healthInfo,
            Address address,
            AccountInfo accountInfo)
        {
            var pet = _pets.FirstOrDefault(x => x.Id == id);
            if (pet is null)
                return Errors.General.NotFound(nameof(PetId)).ToErrorList();
            
            pet.Update(
                nickName,
                speciesAndBreed,
                status,
                generalInfo,
                healthInfo,
                address,
                accountInfo);
            
            return Result.Success();
        }

        public void UpdatePetStatus(Pet.Pet pet, PetStatus status)
        {
            pet.ChangeStatus(status);
        }
    }
}

