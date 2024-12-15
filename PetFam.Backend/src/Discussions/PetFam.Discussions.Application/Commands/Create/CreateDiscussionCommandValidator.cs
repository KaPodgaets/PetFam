using FluentValidation;
using PetFam.Discussions.Domain;
using PetFam.Shared.Validation;

namespace PetFam.Discussions.Application.Commands.Create;

public class CreateDiscussionCommandValidator:AbstractValidator<CreateDiscussionCommand>
{
    public CreateDiscussionCommandValidator()
    {
        RuleFor(x => x.RelationId).NotEmpty().WithMessage("Id can not be empty");

        RuleForEach(x => x.Users)
            .MustBeValueObject(x => User.Create(x.UserId, x.Name));
    }
}
