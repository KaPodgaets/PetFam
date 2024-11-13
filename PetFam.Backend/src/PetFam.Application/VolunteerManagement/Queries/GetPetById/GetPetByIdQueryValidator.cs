using FluentValidation;

namespace PetFam.Application.VolunteerManagement.Queries.GetPetById;

public class GetPetByIdQueryValidator:AbstractValidator<GetPetByIdQuery>
{
    public GetPetByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}