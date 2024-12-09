using FluentValidation;

namespace PetFam.VolunteeringApplications.Application.Commands.Shared;

public class RejectApplicationCommandValidator : AbstractValidator<RejectApplicationCommand>
{
    public RejectApplicationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ID can not be empty");
        RuleFor(x => x.Comment).NotEmpty().WithMessage("Reject comment can not be empty");
    }
}