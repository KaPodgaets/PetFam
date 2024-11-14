﻿using FluentValidation;
using PetFam.Shared.SharedKernel.ValueObjects.Pet;
using PetFam.Shared.Validation;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.CreatePet
{
    public class CreatePetCommandValidator: AbstractValidator<CreatePetCommand>
    {
        public CreatePetCommandValidator()
        {
            RuleFor(x => x.VolunteerId).NotEmpty();
            RuleFor(x => x.NickName).NotEmpty();
            RuleFor(x => x.BreedName).NotEmpty();
            RuleFor(x => x.SpeciesName).NotEmpty();

            RuleFor(x => x.PetGeneralInfoDto)
                .MustBeValueObject(dto => PetGeneralInfo.Create(
                    dto.Comment,
                    dto.Color,
                    dto.Weight,
                    dto.Height,
                    dto.PhoneNumber));

            RuleFor(x => x.PetHealthInfoDto)
                .MustBeValueObject(dto => PetHealthInfo.Create(
                    dto.Comment,
                    dto.IsCastrated,
                    dto.BirthDate,
                    dto.IsVaccinated,
                    dto.Age));

            RuleFor(x => x.AccountInfoDto)
                .MustBeValueObject(dto => AccountInfo.Create(dto.Number, dto.BankName));

            RuleFor(x => x.AddressDto)
                .MustBeValueObject(dto => Address.Create(dto.Country, dto.City, dto.Street, dto.Building, dto.Litteral));
        }
    }
}