using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Volunteer;

namespace PetFam.Application.Volunteers.UpdateSocialMedia
{
    internal class UpdateSocialMediaValidator : AbstractValidator<UpdateSocialMediaRequest>
    {
        public UpdateSocialMediaValidator()
        {
            RuleFor(v => v.Id).NotEmpty();

            RuleForEach(v => v.Dto.SocialMediaLinks)
                .MustBeValueObject(x => SocialMediaLink.Create(x.Name, x.Link));
        }

    }
}
