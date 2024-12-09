using FluentValidation;

namespace PetFam.VolunteeringApplications.Application.Commands;

public class ChangeApplicationStatusCommandValidator:AbstractValidator<ChangeApplicationStatusCommand>
{
    public ChangeApplicationStatusCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
    }
}