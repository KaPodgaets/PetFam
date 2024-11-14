using FluentValidation;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.Delete
{
    public class DelteCommandValidator : AbstractValidator<DeleteCommand>
    {
        public DelteCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
