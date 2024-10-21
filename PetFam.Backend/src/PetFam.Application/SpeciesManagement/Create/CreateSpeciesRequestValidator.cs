using FluentValidation;

namespace PetFam.Application.SpeciesManagement.Create
{
    public class CreateSpeciesRequestValidator : AbstractValidator<CreateSpeciesRequest>
    {
        public CreateSpeciesRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
