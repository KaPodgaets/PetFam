using PetFam.Shared.SharedKernel;
using PetFam.Shared.SharedKernel.Abstractions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;
using PetFam.Shared.SharedKernel.ValueObjects.Species;

namespace PetFam.PetManagement.Domain.Entities
{

    public class Pet : Entity<PetId>, ISoftDeletable
    {
        private List<PetPhoto> _photos = [];
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
            List<PetPhoto> photos
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
            _photos = photos;
        }

        public string NickName { get; private set; } = string.Empty;
        public SpeciesBreed SpeciesAndBreed { get; private set; }
        public PetStatus Status { get; private set; }
        public PetGeneralInfo GeneralInfo { get; private set; }
        public PetHealthInfo HealthInfo { get; private set; }
        public Address Address { get; private set; }
        public AccountInfo AccountInfo { get; private set; }
        public DateTime CreateDate { get; private set; }
        public IReadOnlyList<PetPhoto> Photos => _photos;
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

        internal void Update(
            string nickName,
            SpeciesBreed speciesAndBreed,
            PetStatus status,
            PetGeneralInfo generalInfo,
            PetHealthInfo healthInfo,
            Address address,
            AccountInfo accountInfo)
        {
            NickName = nickName; 
            SpeciesAndBreed = speciesAndBreed;
            Status = status;
            GeneralInfo = generalInfo;
            HealthInfo = healthInfo;
            Address = address;
            AccountInfo = accountInfo;
        }
        
        public Result AddPhotos(IEnumerable<PetPhoto> photos)
        {
            _photos.AddRange(photos);
            
            // keep main photo first 
            _photos = _photos.OrderByDescending(p => p.IsMain).ToList();
            
            return Result.Success();
        }

        public void DeletePhotos(IEnumerable<PetPhoto> photosToDelete)
        {
            photosToDelete = photosToDelete.ToList();

            var pathsToDelete = new HashSet<string>(photosToDelete
                .Select(p => p.FilePath));
                
            _photos.RemoveAll(p => pathsToDelete.Contains(p.FilePath));
        }

        internal Result ChangeMainPhoto(string path)
        {
            var oldPhoto = _photos.FirstOrDefault(p => p.FilePath == path);
            if(oldPhoto is null)
                return Errors.Pet.PhotoNotFound().ToErrorList();
            
            // change status of previous main photo id exist
            var oldMainPhoto = _photos.FirstOrDefault(p => p.IsMain == true);
            if (oldMainPhoto != null)
            {
                var oldMainPhotoWithNewProperty = PetPhoto.Create(oldMainPhoto.FilePath).Value;
                _photos.Remove(oldMainPhoto);
                _photos.Add(oldMainPhotoWithNewProperty);
            }
            
            // set new main photo
            _photos.Remove(oldPhoto);
            
            var mainPhoto = PetPhoto.Create(path, true).Value;
            _photos.Add(mainPhoto);
            
            // keep main photo first
            _photos = _photos.OrderByDescending(p => p.IsMain).ToList();
            
            return Result.Success();
        }

        public new void Delete()
        {
            IsDeleted = true;
            
            if(_photos.Count > 0)
                DeletePhotos(_photos);
        }
        internal void ChangeOrderNumber(int orderNumber)
        {
            Order = orderNumber;
        }

        internal void ChangeStatus(PetStatus status)
        {
            Status = status;
        }
    }
}
