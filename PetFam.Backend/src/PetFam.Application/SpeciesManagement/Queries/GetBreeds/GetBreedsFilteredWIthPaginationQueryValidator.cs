using FluentValidation;

namespace PetFam.Application.SpeciesManagement.Queries.GetBreeds;

public class GetBreedsFilteredWIthPaginationQueryValidator
    : AbstractValidator<GetBreedsFilteredWIthPaginationQuery>
{
    public GetBreedsFilteredWIthPaginationQueryValidator()
    {
        RuleFor(q => q.SpeciesId)
            .NotEmpty().WithMessage("SpeciesId cannot be empty");
    }
}