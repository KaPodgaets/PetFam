using FluentValidation;

namespace PetFam.Application.SpeciesManagement.Delete
{
    public class DeleteSpeciesCommandValidator : AbstractValidator<DeleteSpeciesCommand>
    {
        public DeleteSpeciesCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
