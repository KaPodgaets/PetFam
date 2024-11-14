using FluentValidation;
using PetFam.Shared.SharedKernel.ValueObjects.Volunteer;
using PetFam.Shared.Validation;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.UpdateSocialMedia
{
    public class UpdateSocialMediaCommandValidator : AbstractValidator<UpdateSocialMediaCommand>
    {
        public UpdateSocialMediaCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleForEach(v => v.SocialMediaLinks)
                .MustBeValueObject(x => SocialMediaLink.Create(x.Name, x.Link));
        }

    }
}
