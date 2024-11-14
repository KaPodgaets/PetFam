namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.PetStatusUpdate;

public class PetStatusUpdateCommandValidator : AbstractValidator<PetStatusUpdateCommand>
{
    public PetStatusUpdateCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty();
        RuleFor(x => x.PetId).NotEmpty();

        RuleFor(x => x.NewPetStatus)
            .Must(ValidatorRulesExtension.BeValidPetStatus<PetStatus>)
            .WithMessage("Invalid status");
    }
}