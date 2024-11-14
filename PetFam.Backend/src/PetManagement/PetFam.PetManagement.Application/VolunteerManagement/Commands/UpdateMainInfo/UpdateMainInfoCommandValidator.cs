using FluentValidation;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;
using PetFam.Shared.Validation;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.UpdateMainInfo
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
