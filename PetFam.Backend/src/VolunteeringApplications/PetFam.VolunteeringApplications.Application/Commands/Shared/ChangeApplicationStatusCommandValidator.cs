using FluentValidation;

namespace PetFam.VolunteeringApplications.Application.Commands.Shared;

public class ChangeApplicationStatusCommandValidator:AbstractValidator<ChangeApplicationStatusCommand>
{
    public ChangeApplicationStatusCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
    }
}