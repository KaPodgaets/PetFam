using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.VolunteerManagement.Commands.UpdateRequisites
{
    public class UpdateRequisitesCommandValidator : AbstractValidator<UpdateRequisitesCommand>
    {
        public UpdateRequisitesCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleForEach(v => v.Requisites)
                .MustBeValueObject(x => Requisite.Create(x.Name, x.AccountNumber, x.PaymentInstruction));
        }
    }
}
