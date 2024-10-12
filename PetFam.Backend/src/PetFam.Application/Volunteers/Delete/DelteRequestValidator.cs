using FluentValidation;

namespace PetFam.Application.Volunteers.Delete
{
    public class DelteRequestValidator : AbstractValidator<DeleteRequest>
    {
        public DelteRequestValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
