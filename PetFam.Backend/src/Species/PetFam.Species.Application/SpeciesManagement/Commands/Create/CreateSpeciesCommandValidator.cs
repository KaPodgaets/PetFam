namespace PetFam.Species.Application.SpeciesManagement.Commands.Create
{
    public class CreateSpeciesCommandValidator : AbstractValidator<CreateSpeciesCommand>
    {
        public CreateSpeciesCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
