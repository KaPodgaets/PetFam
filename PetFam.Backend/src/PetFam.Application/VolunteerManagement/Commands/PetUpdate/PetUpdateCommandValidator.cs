using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.SpeciesManagement;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Application.VolunteerManagement.Commands.PetUpdate;

public class PetUpdateCommandValidator : AbstractValidator<PetUpdateCommand> 
{
    public PetUpdateCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty();
        RuleFor(x => x.PetId).NotEmpty();
        RuleFor(x => x.NickName).NotEmpty();
        
        RuleFor(x => x.GeneralInfo).MustBeValueObject(dto =>
            PetGeneralInfo.Create(dto.Comment, dto.Color, dto.Weight, dto.Height,dto.PhoneNumber));

        RuleFor(x => x.SpeciesAndBreed)
            .MustBeValueObject(
                dto => SpeciesBreed.Create(SpeciesId.Create(dto.SpeciesId),
                dto.BreedId));

        RuleFor(x => x.Status)
            .Must(BeAValidStatus)
            .WithMessage("Invalid status");

        RuleFor(x => x.HealthInfo)
            .MustBeValueObject(dto => PetHealthInfo.Create(
                dto.Comment,
                dto.IsCastrated,
                dto.BirthDate,
                dto.IsVaccinated));

        RuleFor(x => x.Address)
            .MustBeValueObject(dto => Address.Create(
                dto.Country,
                dto.City,
                dto.Street,
                dto.Building,
                dto.Litteral));

        RuleFor(x => x.AccountInfo)
            .MustBeValueObject(dto => AccountInfo.Create(
                dto.Number,
                dto.BankName));
    }    
    
    private static bool BeAValidStatus(int statusValue)
    {
        return Enum.IsDefined(typeof(PetStatus), statusValue);
    }
}