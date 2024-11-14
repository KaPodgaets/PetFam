using FluentValidation;

namespace PetFam.PetManagement.Application.VolunteerManagement.Commands.AddPetPhotos
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
