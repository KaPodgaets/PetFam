using FluentValidation;

namespace PetFam.VolunteeringApplications.Application.Queries.GetById;

public class GetByIdQueryValidator :AbstractValidator<GetByIdQuery>
{
    public GetByIdQueryValidator()
    {
        RuleFor(q => q.Id).NotEmpty();
    }
}