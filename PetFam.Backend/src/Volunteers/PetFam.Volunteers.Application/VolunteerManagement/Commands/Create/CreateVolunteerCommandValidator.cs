namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.Create
{
    public class CreateVolunteerCommandValidator : AbstractValidator<CreateVolunteerCommand>
    {
        public CreateVolunteerCommandValidator()
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
