using FluentValidation;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Queries.Get;

public class GetWithPaginationQueryValidator : AbstractValidator<GetWithPaginationQuery>
{
    public GetWithPaginationQueryValidator()
    {
        RuleFor(query => query.UserId)
            .Must(userId => userId == null || userId != Guid.Empty)
            .WithMessage("UserId must be a valid GUID if provided.");

        RuleFor(query => query.AdminId)
            .Must(adminId => adminId == null || adminId != Guid.Empty)
            .WithMessage("AdminId must be a valid GUID if provided.");

        RuleFor(query => query.Status)
            .Must(status => status == null || Enum.IsDefined(typeof(VolunteeringApplicationStatus), status))
            .WithMessage("Status must be a valid VolunteeringApplicationStatus value.");

        RuleFor(query => query.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0.");

        RuleFor(query => query.PageSize)
            .GreaterThan(0)
            .WithMessage("PageSize must be greater than 0.")
            .LessThanOrEqualTo(100)
            .WithMessage("PageSize must not exceed 100.");
    }
}