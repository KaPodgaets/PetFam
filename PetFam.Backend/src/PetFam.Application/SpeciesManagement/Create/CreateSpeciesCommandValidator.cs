using FluentValidation;

namespace PetFam.Application.SpeciesManagement.Create
{
    public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
    {
        public CreateSpeciesCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
