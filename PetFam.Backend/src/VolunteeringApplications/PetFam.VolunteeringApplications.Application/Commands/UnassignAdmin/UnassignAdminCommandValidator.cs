using FluentValidation;
using PetFam.VolunteeringApplications.Application.Commands.AssignAdmin;

namespace PetFam.VolunteeringApplications.Application.Commands.UnassignAdmin;

public class UnassignAdminCommandValidator : AbstractValidator<AssignAdminCommand>
{
    public UnassignAdminCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Application Id cannot be empty");
        RuleFor(x => x.AdminId).NotEmpty().WithMessage("User Id cannot be empty");
    }
}