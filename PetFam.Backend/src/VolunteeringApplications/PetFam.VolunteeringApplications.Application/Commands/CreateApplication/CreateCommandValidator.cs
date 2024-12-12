using FluentValidation;

namespace PetFam.VolunteeringApplications.Application.Commands.CreateApplication;

public class CreateCommandValidator:AbstractValidator<CreateCommand>
{
    public CreateCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User Id cannot be empty");
        RuleFor(x => x.VolunteerInfo).NotEmpty().WithMessage("Volunteer Info cannot be empty");
    }
}