using System.Drawing;
using FluentValidation;
using PetFam.Application.Validation;
using PetFam.Domain.Volunteer.Pet;

namespace PetFam.Application.VolunteerManagement.Commands.DeletePetPhotos;

public class DeletePetPhotosCommandValidator:AbstractValidator<DeletePetPhotosCommand>
{
    public DeletePetPhotosCommandValidator()
    {
        RuleFor(x => x.Paths)
            .ForEach(path => path.NotEmpty());
        RuleFor(x => x.Paths)
            .ForEach(x => x.MustBeValueObject(PetPhoto.Create));
        
        RuleFor(x => x.PetId).NotEmpty();
        RuleFor(x => x.VolunteerId).NotEmpty();
        
    }
}