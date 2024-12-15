using FluentValidation;

namespace PetFam.Discussions.Application.Commands.DeleteMessage;

public class DeleteMessageCommandValidator:AbstractValidator<DeleteMessageCommand>
{
    public DeleteMessageCommandValidator()
    {
        RuleFor(x => x.MessageId).NotEmpty().WithMessage("Message Id can not be empty");
        RuleFor(x => x.DiscussionId).NotEmpty().WithMessage("Discussion Id can not be empty");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Author Id can not be empty");
    }
}
