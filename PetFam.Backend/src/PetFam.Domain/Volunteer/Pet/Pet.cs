using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;

namespace PetFam.Domain.Volunteer.Pet
{
    public record PetGeneralInfo(
        string Comment,
        string Color,
        double Weight,
        double Height,
        Address Address,
        string PhoneNumber,
        AccountInfo AccountInfo);

    public record PetHealthInfo(
        string Comment,
        bool IsCastrated,
        DateTime BirthDate,
        bool IsVaccinated);

    public class Pet : Entity<PetId>, ISoftDeletable
    {
        private bool _isDeleted = false;
        private Pet(PetId id) : base(id)
        {
        }

        private Pet(PetId petId,
            string nickName,
            SpeciesBreed speciesAndBreed,
            PetStatus status,
            PetGeneralInfo generalInfo,
            PetHealthInfo healthInfo,
            DateTime createDate
            ) : base(petId)
        {
            NickName = nickName;
            SpeciesAndBreed = speciesAndBreed;
            Status = status;
            GeneralInfo = generalInfo;
            HealthInfo = healthInfo;
            CreateDate = createDate;
        }

        public string NickName { get; private set; } = string.Empty;
        public SpeciesBreed SpeciesAndBreed { get; private set; }
        public PetStatus Status { get; private set; }
        public PetGeneralInfo GeneralInfo { get; private set; }
        public PetHealthInfo HealthInfo { get; private set; }
        public DateTime CreateDate { get; private set; }
        public Gallery? Gallery { get; private set; }

        public static Result<Pet> Create(PetId petId,
            string nickName,
            SpeciesBreed speciesAndBreed,
            PetStatus status,
            PetGeneralInfo generalInfo,
            PetHealthInfo healthInfo,
            DateTime createDate
            )
        {
            if (petId.Value == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(PetId));

            if (string.IsNullOrWhiteSpace(nickName))
                return Errors.General.ValueIsInvalid(nameof(NickName));

            if (nickName.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsInvalid(nameof(NickName));

            return new Pet(petId,
            nickName,
            speciesAndBreed,
            status,
            generalInfo,
            healthInfo,
            createDate);
        }

        public void Delete()
        {
            _isDeleted = true;
        }

        public void Restore()
        {
            _isDeleted = false;
        }
    }
}
