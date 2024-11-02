using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.VolunteerManagement.UpdateSocialMedia
{
    public class UpdateSocialMediaCommandValidator : AbstractValidator<UpdateSocialMediaCommand>
    {
        public UpdateSocialMediaCommandValidator()
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleForEach(v => v.Dto.SocialMediaLinks)
                .MustBeValueObject(x => SocialMediaLink.Create(x.Name, x.Link));
        }

    }
}
