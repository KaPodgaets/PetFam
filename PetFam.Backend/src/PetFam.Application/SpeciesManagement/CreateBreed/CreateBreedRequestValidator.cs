using FluentValidation;

namespace PetFam.Application.SpeciesManagement.CreateBreed
{
    public class CreateBreedRequestValidator: AbstractValidator<CreateBreedRequest>
    {
        public CreateBreedRequestValidator()
        {
            RuleFor(x => x.SpeciesId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
