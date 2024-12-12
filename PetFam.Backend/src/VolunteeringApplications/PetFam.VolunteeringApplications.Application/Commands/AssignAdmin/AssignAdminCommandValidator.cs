using FluentValidation;
using PetFam.Shared.Abstractions;

namespace PetFam.VolunteeringApplications.Application.Commands.AssignAdmin;

public class AssignAdminCommandValidator : AbstractValidator<AssignAdminCommand>
{
    public AssignAdminCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Application Id cannot be empty");
        RuleFor(x => x.AdminId).NotEmpty().WithMessage("User Id cannot be empty");
    }
}