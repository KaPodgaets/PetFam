using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.VolunteerManagement.Create
{
    public class CreateVolunteerRequestValidator : AbstractValidator<CreateVolunteerRequest>
    {
        public CreateVolunteerRequestValidator()
        {
            RuleFor(c => c.FullNameDto).MustBeValueObject(x =>
                FullName.Create(x.FirstName, x.LastName, x.Patronimycs));

            RuleFor(r => r.Email).MustBeValueObject(Email.Create);

            RuleForEach(c => c.Requisites)
                .MustBeValueObject(x => Requisite.Create(x.Name, x.AccountNumber, x.PaymentInstruction));

            RuleForEach(c => c.SocialMediaLinks)
                .MustBeValueObject(x => SocialMediaLink.Create(x.Name, x.Link));
        }
    }
}
