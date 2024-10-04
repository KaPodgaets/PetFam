using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Shared;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.UpdateName
{
    public class VolunteerUpdateMainInfoValidator : AbstractValidator<UpdateMainInfoRequest>
    {
        public VolunteerUpdateMainInfoValidator()
        {
            RuleFor(v => v.Dto.FullNameDto).MustBeValueObject(x =>
                FullName.Create(x.FirstName, x.LastName, x.Patronimycs));

            RuleFor(v => v.Id).NotEmpty()
                .WithError(Errors.General.ValueIsRequired());

        }
    }
}
