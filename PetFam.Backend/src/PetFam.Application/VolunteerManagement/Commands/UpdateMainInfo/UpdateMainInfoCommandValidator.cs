using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Volunteer;
using PetFam.Shared.Shared;
using PetFam.Shared.ValueObjects.Volunteer;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateMainInfo
{
    public class UpdateMainInfoCommandValidator : AbstractValidator<UpdateMainInfoCommand>
    {
        public UpdateMainInfoCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleFor(v => v.FullNameDto).MustBeValueObject(x =>
                FullName.Create(x.FirstName, x.LastName, x.Patronimycs));

            RuleFor(v => v.Id).NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(v => v.GeneralInformationDto).MustBeValueObject(x =>
                GeneralInformation.Create(x.BioEducation, x.ShortDescription));

            RuleFor(v => v.Email).MustBeValueObject(Email.Create);

            RuleFor(v => v.AgeOfExperience).GreaterThanOrEqualTo(0);
        }
    }
}
