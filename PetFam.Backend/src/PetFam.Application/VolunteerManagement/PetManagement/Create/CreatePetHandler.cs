using Microsoft.Extensions.Logging;
using PetFam.Application.SpeciesManagement;
using PetFam.Domain.Shared;
using PetFam.Domain.SpeciesManagement;
using PetFam.Domain.Volunteer;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Application.VolunteerManagement.PetManagement.Create
{
    public class CreatePetHandler
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly ILogger _logger;

        public CreatePetHandler(
            IVolunteerRepository repository,
            ISpeciesRepository speciesRepository,
            ILogger<CreatePetHandler> logger)
        {
            _volunteerRepository = repository;
            _speciesRepository = speciesRepository;
            _logger = logger;
        }
        public async Task<Result<Guid>> Execute(CreatePetCommand request,
            CancellationToken cancellationToken = default)
        {
            // get volunteer by id

            var volunteerId = VolunteerId.Create(request.VolunteerId);

            var getVolunteerResult = await _volunteerRepository.GetById(volunteerId, cancellationToken);

            if(getVolunteerResult.IsFailure)
            {
                return Errors.General.NotFound(volunteerId.Value);
            }

            var volunteer = getVolunteerResult.Value;

            // check breed and species exists
            var getSpeciesResult = await _speciesRepository.GetByName(request.CreatePetDto.SpeciesName, cancellationToken);
            if (getSpeciesResult.IsFailure)
            {
                return Errors.General.NotFound(request.CreatePetDto.SpeciesName);
            }
            var species = getSpeciesResult.Value;

            var breedId = getSpeciesResult.Value.Breeds.Where(x => x.Name == request.CreatePetDto.BreedName).Select(x => x.Id).FirstOrDefault();

            if (breedId == null)
            {
                return Errors.General.NotFound(request.CreatePetDto.BreedName);
            }

            var speciesBreed = SpeciesBreed.Create(getSpeciesResult.Value.Id, breedId.Value);

            // create pet values objects
            var generalInfoDto = request.CreatePetDto.PetGeneralInfoDto;

            var generalInfo = PetGeneralInfo.Create(
                generalInfoDto.Comment, 
                generalInfoDto.Color, 
                generalInfoDto.Weight, 
                generalInfoDto.Height, 
                generalInfoDto.PhoneNumber)
                    .Value;

            var healthInfoDto = request.CreatePetDto.PetHealthInfoDto;

            var healthInfo = PetHealthInfo.Create(
                healthInfoDto.Comment, 
                healthInfoDto.IsCastrated, 
                healthInfoDto.BirthDate.ToUniversalTime(), 
                healthInfoDto.IsVaccinated)
                    .Value;

            var addressDto = request.CreatePetDto.AddressDto;

            var address = Address.Create(
                addressDto.Country, 
                addressDto.City, 
                addressDto.Street, 
                addressDto.Building, 
                addressDto.Litteral)
                    .Value;

            var accountInfoDto = request.CreatePetDto.AccountInfoDto;

            var accountInfo = AccountInfo.Create(
                accountInfoDto.Number,
                accountInfoDto.BankName)
                    .Value;

            // create pet entity

            var createPetResult = Pet.Create(
                PetId.NewPetId(),
                request.CreatePetDto.NickName,
                speciesBreed.Value,
                PetStatus.LookingForHome,
                generalInfo,
                healthInfo,
                address,
                accountInfo,
                DateTime.Now.ToUniversalTime(),
                volunteer.Pets.Count);

            if (createPetResult.IsFailure)
                return Result<Guid>.Failure(createPetResult.Error);

            // add pet to volunteer

            volunteer.AddPet(createPetResult.Value);

            var result = await _volunteerRepository.Update(volunteer, cancellationToken);

            if(result.IsFailure)
            {
                return Result<Guid>.Failure(result.Error);
            }

            _logger.LogInformation(
                "To volunteer with {id} added pet with {petId}",
                volunteer.Id.Value,
                createPetResult.Value.Id.Value);

            
            return createPetResult.Value.Id.Value;
        }
    }
}
