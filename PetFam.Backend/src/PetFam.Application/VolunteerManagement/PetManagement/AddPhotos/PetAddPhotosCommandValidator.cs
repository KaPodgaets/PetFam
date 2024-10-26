using FluentValidation;

namespace PetFam.Application.VolunteerManagement.PetManagement.AddPhotos
{
    public class PetAddPhotosCommandValidator : AbstractValidator<PetAddPhotosCommand>
    {
        public PetAddPhotosCommandValidator()
        {
            RuleFor(x => x.VolunteerId).NotEmpty();
            RuleFor(x => x.PetId).NotEmpty();
        }
    }
}
