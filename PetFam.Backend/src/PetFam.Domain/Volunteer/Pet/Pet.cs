using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;

namespace PetFam.Domain.Volunteer.Pet
{

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
            Address address,
            AccountInfo accountInfo,
            DateTime createDate,
            int order,
            ValueObjectList<PetPhoto> photos
            ) : base(petId)
        {
            NickName = nickName;
            SpeciesAndBreed = speciesAndBreed;
            Status = status;
            GeneralInfo = generalInfo;
            HealthInfo = healthInfo;
            Address = address;
            AccountInfo = accountInfo;
            CreateDate = createDate;
            Order = order;
            Photos = photos;
        }

        public string NickName { get; private set; } = string.Empty;
        public SpeciesBreed SpeciesAndBreed { get; private set; }
        public PetStatus Status { get; private set; }
        public PetGeneralInfo GeneralInfo { get; private set; }
        public PetHealthInfo HealthInfo { get; private set; }
        public Address Address { get; private set; }
        public AccountInfo AccountInfo { get; private set; }
        public DateTime CreateDate { get; private set; }
        public ValueObjectList<PetPhoto> Photos { get; private set; }
        public int Order { get;private set; }

        public static Result<Pet> Create(PetId petId,
            string nickName,
            SpeciesBreed speciesAndBreed,
            PetStatus status,
            PetGeneralInfo generalInfo,
            PetHealthInfo healthInfo,
            Address address,
            AccountInfo accountInfo,
            DateTime createDate,
            int order)
        {
            if (petId.Value == Guid.Empty)
                return Errors.General.ValueIsInvalid(nameof(PetId)).ToErrorList();

            if (string.IsNullOrWhiteSpace(nickName))
                return Errors.General.ValueIsInvalid(nameof(NickName)).ToErrorList();

            if (nickName.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsInvalid(nameof(NickName)).ToErrorList();

            if (order < 0)
                return Errors.General.ValueIsInvalid(nameof(Order)).ToErrorList();

            return new Pet(petId,
                nickName,
                speciesAndBreed,
                status,
                generalInfo,
                healthInfo,
                address,
                accountInfo,
                createDate,
                order,
                new ValueObjectList<PetPhoto>([])
            );
        }

        public Result AddPhotos(List<PetPhoto> photos)
        {
            if (photos == null || photos.Count == 0)
            {
                return Errors.General.ValueIsRequired("Photos not provided").ToErrorList();
            }

            if (Photos == null)
            {
                ValueObjectList<PetPhoto> newPhotos = new(photos);
                Photos = newPhotos;
                return Result.Success();
            }

            var updatedPhotos = Photos.ToList();
            updatedPhotos.AddRange(photos);

            Photos = new ValueObjectList<PetPhoto>(updatedPhotos);
            return Result.Success();
        }

        public void Delete()
        {
            _isDeleted = true;
        }

        public void Restore()
        {
            _isDeleted = false;
        }

        internal void ChangeOrderNumber(int orderNumber)
        {
            Order = orderNumber;
        }
    }
}
