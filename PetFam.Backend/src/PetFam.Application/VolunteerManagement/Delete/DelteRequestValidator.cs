using FluentValidation;

namespace PetFam.Application.VolunteerManagement.Delete
{
    public class DelteRequestValidator : AbstractValidator<DeleteRequest>
    {
        public DelteRequestValidator()
        {
            RuleFor(v => v.Id).NotEmpty();
        }
    }
}
