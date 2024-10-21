using FluentValidation;

namespace PetFam.Application.SpeciesManagement.Delete
{
    public class DeleteSpeciesRequestValidator : AbstractValidator<DeleteSpeciesRequest>
    {
        public DeleteSpeciesRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
