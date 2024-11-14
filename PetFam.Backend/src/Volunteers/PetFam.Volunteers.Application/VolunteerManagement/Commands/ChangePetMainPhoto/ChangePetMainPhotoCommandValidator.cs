namespace PetFam.Volunteers.Application.VolunteerManagement.Commands.ChangePetMainPhoto;

public class ChangePetMainPhotoCommandValidator:AbstractValidator<ChangePetMainPhotoCommand>
{
    public ChangePetMainPhotoCommandValidator()
    {
        RuleFor(x => x.Path).NotEmpty();
        RuleFor(x => x.VolunteerId).NotEmpty();
        RuleFor(x => x.PetId).NotEmpty();
    }
}