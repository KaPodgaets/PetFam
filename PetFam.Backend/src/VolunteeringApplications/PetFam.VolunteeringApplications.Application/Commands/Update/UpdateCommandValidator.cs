using FluentValidation;

namespace PetFam.VolunteeringApplications.Application.Commands.Update;

public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
{
    public UpdateCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
        RuleFor(x => x.VolunteerInfo).NotEmpty().WithMessage("Volunteer Info cannot be empty");
    }
    
}