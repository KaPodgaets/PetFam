using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.UpdateMainInfo
{
    public class UpdateMainInfoValidator : AbstractValidator<UpdateMainInfoRequest>
    {
        public UpdateMainInfoValidator()
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleFor(v => v.Dto.FullNameDto).MustBeValueObject(x =>
                FullName.Create(x.FirstName, x.LastName, x.Patronimycs));

            RuleFor(v => v.Id).NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

            RuleFor(v => v.Dto.GeneralInformationDto).MustBeValueObject(x =>
                GeneralInformation.Create(x.BioEducation, x.ShortDescription));

            RuleFor(v => v.Dto.Email).MustBeValueObject(x =>
                Email.Create(x));

            RuleFor(v => v.Dto.AgeOfExpirience).GreaterThanOrEqualTo(0);
        }
    }
}
