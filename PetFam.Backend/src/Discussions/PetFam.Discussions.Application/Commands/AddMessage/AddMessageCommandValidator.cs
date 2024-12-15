using FluentValidation;

namespace PetFam.Discussions.Application.Commands.AddMessage;

public class AddMessageCommandValidator:AbstractValidator<AddMessageCommand>
{
    public AddMessageCommandValidator()
    {
        RuleFor(x => x.MessageText).NotEmpty().WithMessage("Message can not be empty");
        RuleFor(x => x.DiscussionId).NotEmpty().WithMessage("DiscussionId can not be empty");
    }
}
