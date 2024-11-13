using FluentValidation;

namespace PetFam.Application.VolunteerManagement.Commands.DeletePet;

public class DeletePetCommandValidator:AbstractValidator<DeletePetCommand>
{
    public DeletePetCommandValidator()
    {
        RuleFor(x => x.VolunteerId).NotEmpty();
        RuleFor(x => x.PetId).NotEmpty();
        
    }
}