using FluentValidation;

namespace PetFam.Application.SpeciesManagement.Commands.Create
{
    public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
    {
        public CreateSpeciesCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
