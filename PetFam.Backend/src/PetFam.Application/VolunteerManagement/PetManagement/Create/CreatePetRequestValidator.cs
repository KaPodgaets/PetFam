using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Application.VolunteerManagement.PetManagement.Create
{
    public class CreatePetRequestValidator: AbstractValidator<CreatePetRequest>
    {
        public CreatePetRequestValidator()
        {
            RuleFor(x => x.VolunteerId).NotEmpty();
            RuleFor(x => x.CreatePetDto.NickName).NotEmpty();
            RuleFor(x => x.CreatePetDto.BreedName).NotEmpty();
            RuleFor(x => x.CreatePetDto.SpeciesName).NotEmpty();

            RuleFor(x => x.CreatePetDto.PetGeneralInfoDto)
                .MustBeValueObject(dto => PetGeneralInfo.Create(
                    dto.Comment,
                    dto.Color,
                    dto.Weight,
                    dto.Height,
                    dto.PhoneNumber));

            RuleFor(x => x.CreatePetDto.PetHealthInfoDto)
                .MustBeValueObject(dto => PetHealthInfo.Create(
                    dto.Comment,
                    dto.IsCastrated,
                    dto.BirthDate,
                    dto.IsVaccinated));

            RuleFor(x => x.CreatePetDto.AccountInfoDto)
                .MustBeValueObject(dto => AccountInfo.Create(dto.Number, dto.BankName));

            RuleFor(x => x.CreatePetDto.AddressDto)
                .MustBeValueObject(dto => Address.Create(dto.Country, dto.City, dto.Street, dto.Building, dto.Litteral));
        }
    }
}
