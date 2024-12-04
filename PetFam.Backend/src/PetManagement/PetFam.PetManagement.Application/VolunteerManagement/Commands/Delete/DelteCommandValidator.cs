using FluentValidation;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.Delete
{
    public class DelteCommandValidator : AbstractValidator<DeleteVolunteerCommand>
    {
        public DelteCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
