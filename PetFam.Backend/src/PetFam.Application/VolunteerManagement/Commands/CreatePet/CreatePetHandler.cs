using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.Application.Database;
using PetFam.Application.SpeciesManagement;
using PetFam.Domain.Volunteer.Pet;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.Shared;
using PetFam.Shared.ValueObjects.Pet;
using PetFam.Shared.ValueObjects.Species;
using PetFam.Shared.ValueObjects.Volunteer;

namespace PetFam.Application.VolunteerManagement.Commands.CreatePet
{
    public class CreatePetHandler:ICommandHandler<Guid, CreatePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<CreatePetCommand> _validator;
        private readonly ISpeciesRepository _speciesRepository;
        private readonly IReadDbContext _readDbContext;
        private readonly ILogger _logger;

        public CreatePetHandler(
            IVolunteerRepository repository,
            ISpeciesRepository speciesRepository,
            ILogger<CreatePetHandler> logger,
            IValidator<CreatePetCommand> validator,
            IReadDbContext readDbContext)
        {
            _volunteerRepository = repository;
            _speciesRepository = speciesRepository;
            _logger = logger;
            _validator = validator;
            _readDbContext = readDbContext;
        }
        public async Task<Result<Guid>> ExecuteAsync(CreatePetCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var volunteerId = VolunteerId.Create(command.VolunteerId);

            var getVolunteerResult = await _volunteerRepository.GetById(volunteerId, cancellationToken);

            if(getVolunteerResult.IsFailure)
            {
                return Errors.General.NotFound(volunteerId.Value).ToErrorList();
            }

            var volunteer = getVolunteerResult.Value;
            
            // check breed and species exists with ReadDbContext
            var speciesDto = await _readDbContext.Species
                .FirstOrDefaultAsync(s => s.Name == command.SpeciesName, cancellationToken);
            
            if(speciesDto is null)
                return Errors.General.NotFound(command.SpeciesName).ToErrorList();
            
            var breedDto = await _readDbContext.Breeds
                .FirstOrDefaultAsync(b => b.Name == command.BreedName, cancellationToken);
            
            if(breedDto is null)
                return Errors.General.NotFound(command.BreedName).ToErrorList();
            
            var speciesBreed = SpeciesBreed.Create(
                SpeciesId.Create(speciesDto.Id),
                BreedId.Create(breedDto.Id).Value);

            // create pet values objects
            var generalInfoDto = command.PetGeneralInfoDto;

            var generalInfo = PetGeneralInfo.Create(
                generalInfoDto.Comment, 
                generalInfoDto.Color, 
                generalInfoDto.Weight, 
                generalInfoDto.Height, 
                generalInfoDto.PhoneNumber)
                    .Value;

            var healthInfoDto = command.PetHealthInfoDto;

            var healthInfo = PetHealthInfo.Create(
                healthInfoDto.Comment, 
                healthInfoDto.IsCastrated, 
                healthInfoDto.BirthDate.ToUniversalTime(), 
                healthInfoDto.IsVaccinated,
                healthInfoDto.Age)
                    .Value;

            var addressDto = command.AddressDto;

            var address = Address.Create(
                addressDto.Country, 
                addressDto.City, 
                addressDto.Street, 
                addressDto.Building, 
                addressDto.Litteral)
                    .Value;

            var accountInfoDto = command.AccountInfoDto;

            var accountInfo = AccountInfo.Create(
                accountInfoDto.Number,
                accountInfoDto.BankName)
                    .Value;

            // create pet entity

            var createPetResult = Pet.Create(
                PetId.NewPetId(),
                command.NickName,
                speciesBreed.Value,
                PetStatus.LookingForHome,
                generalInfo,
                healthInfo,
                address,
                accountInfo,
                DateTime.Now.ToUniversalTime(),
                volunteer.Pets.Count);

            if (createPetResult.IsFailure)
                return Result<Guid>.Failure(createPetResult.Errors);

            // add pet to volunteer

            volunteer.AddPet(createPetResult.Value);

            var result = await _volunteerRepository.Update(volunteer, cancellationToken);

            if(result.IsFailure)
            {
                return Result<Guid>.Failure(result.Errors);
            }

            _logger.LogInformation(
                "To volunteer with {id} added pet with {petId}",
                volunteer.Id.Value,
                createPetResult.Value.Id.Value);

            
            return createPetResult.Value.Id.Value;
        }
    }
}
