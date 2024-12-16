using FluentValidation;

namespace PetFam.Discussions.Application.Commands.Close;

public class CloseDiscussionCommandValidator:AbstractValidator<CloseDiscussionCommand>
{
    public CloseDiscussionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id can not be empty");
    }
}
