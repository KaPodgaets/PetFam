using FluentValidation;

namespace PetFam.Discussions.Application.Commands.EditeMessage;

public class EditMessageCommandValidator:AbstractValidator<EditMessageCommand>
{
    public EditMessageCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User id can not be empty");
        RuleFor(x => x.DiscussionId).NotEmpty().WithMessage("DiscussionId can not be empty");
        RuleFor(x => x.NewText).NotEmpty().WithMessage("Message can not be empty");
    }
}
