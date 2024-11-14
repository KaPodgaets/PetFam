using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.BreedManagement.Contracts;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;
using PetFam.Shared.SharedKernel.ValueObjects.Species;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;
using PetFam.PetManagement.Domain.Entities;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.CreatePet
{
    public class CreatePetHandler : ICommandHandler<Guid, CreatePetCommand>
    {
        private readonly IVolunteerRepository _volunteerRepository;
        private readonly IValidator<CreatePetCommand> _validator;
        private readonly ILogger _logger;
        private readonly ISpeciesContract _speciesContract;


        public CreatePetHandler(
            IVolunteerRepository repository,
            ILogger<CreatePetHandler> logger,
            IValidator<CreatePetCommand> validator, ISpeciesContract speciesContract)
        {
            _volunteerRepository = repository;
            _logger = logger;
            _validator = validator;
            _speciesContract = speciesContract;
        }

        public async Task<Result<Guid>> ExecuteAsync(CreatePetCommand command,
            CancellationToken cancellationToken = default)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.IsValid == false)
                return validationResult.ToErrorList();

            var volunteerId = VolunteerId.Create(command.VolunteerId);

            var getVolunteerResult = await _volunteerRepository.GetById(volunteerId, cancellationToken);

            if (getVolunteerResult.IsFailure)
            {
                return Errors.General.NotFound(volunteerId.Value).ToErrorList();
            }

            var volunteer = getVolunteerResult.Value;
            
            var speciesId = SpeciesId.Create(command.SpeciesId);
            
            var isBreedExists = await _speciesContract.
                CheckBreedExists(
                    speciesId,
                    BreedId.Create(command.BreedId),
                    cancellationToken);

            if (isBreedExists.Value)
                return Errors.General.NotFound(command.BreedId).ToErrorList();

            var speciesBreed = SpeciesBreed.Create(
                speciesId,
                command.BreedId);

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

            if (result.IsFailure)
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