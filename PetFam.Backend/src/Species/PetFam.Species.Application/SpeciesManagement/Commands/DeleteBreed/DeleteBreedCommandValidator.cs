namespace PetFam.Species.Application.SpeciesManagement.Commands.DeleteBreed;

public class DeleteBreedCommandValidator:AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedCommandValidator()
    {
        RuleFor(command => command.SpeciesId).NotEmpty();
        RuleFor(command => command.BreedId).NotEmpty();
    }
}