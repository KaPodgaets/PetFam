using FluentValidation;

namespace PetFam.BreedManagement.Application.SpeciesManagement.Commands.Create
{
    public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
    {
        public CreateSpeciesCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
