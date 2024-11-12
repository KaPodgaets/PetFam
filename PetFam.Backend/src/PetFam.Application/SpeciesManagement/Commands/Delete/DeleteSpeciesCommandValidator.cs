using FluentValidation;

namespace PetFam.Application.SpeciesManagement.Commands.Delete
{
    public class DeleteSpeciesCommandValidator : AbstractValidator<DeleteSpeciesCommand>
    {
        public DeleteSpeciesCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
