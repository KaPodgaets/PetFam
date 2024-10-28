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
            int order
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
        }

        public string NickName { get; private set; } = string.Empty;
        public SpeciesBreed SpeciesAndBreed { get; private set; }
        public PetStatus Status { get; private set; }
        public PetGeneralInfo GeneralInfo { get; private set; }
        public PetHealthInfo HealthInfo { get; private set; }
        public Address Address { get; private set; }
        public AccountInfo AccountInfo { get; private set; }
        public DateTime CreateDate { get; private set; }
        public Gallery Gallery { get; private set; }
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
                return Errors.General.ValueIsInvalid(nameof(PetId));

            if (string.IsNullOrWhiteSpace(nickName))
                return Errors.General.ValueIsInvalid(nameof(NickName));

            if (nickName.Length > Constants.MAX_LOW_TEXT_LENGTH)
                return Errors.General.ValueIsInvalid(nameof(NickName));

            if(order < 0)
                return Errors.General.ValueIsInvalid(nameof(Order));

            return new Pet(petId,
            nickName,
            speciesAndBreed,
            status,
            generalInfo,
            healthInfo,
            address,
            accountInfo,
            createDate,
            order);
        }

        public Result AddPhotos(List<PetPhoto> photos)
        {
            List<PetPhoto> newPhotos = [..photos];

            foreach(var photo in Gallery.Value)
            {
                var existingPhoto = PetPhoto.Create(photo.FilePath, false).Value;
                newPhotos.Add(existingPhoto);
            }

            var newGalleryResult = Gallery.Create(newPhotos);

            if (newGalleryResult.IsFailure)
            {
                return newGalleryResult.Error;
            }

            Gallery = newGalleryResult.Value;

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

        public void ChangeOrderNumber(int orderNumber)
        {
            Order = orderNumber;
        }
    }
}
