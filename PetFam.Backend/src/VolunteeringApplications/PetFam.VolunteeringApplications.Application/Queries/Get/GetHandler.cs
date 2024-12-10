using FluentValidation;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.Models;
using PetFam.Shared.SharedKernel.Result;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Queries.Get;

public class GetHandler : IQueryHandler<PagedList<VolunteeringApplication>, GetWithPaginationQuery>
{
    private readonly IApplicationsReadDbContext _dbContext;
    private readonly IValidator<GetWithPaginationQuery> _validator;

    public GetHandler(IApplicationsReadDbContext dbContext,
        IValidator<GetWithPaginationQuery> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<Result<PagedList<VolunteeringApplication>>> HandleAsync(
        GetWithPaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();

        var applicationsQuery = _dbContext.Applications.AsQueryable();

        applicationsQuery = applicationsQuery
            .WhereIf(query.AdminId is not null, p => p.AdminId == query.AdminId)
            .WhereIf(query.UserId is not null, p => p.UserId == query.UserId)
            .WhereIf(query.Status is not null, p => p.Status == (VolunteeringApplicationStatus)query.Status!);

        var pagedList = await applicationsQuery
            .ToPagedList(query.PageNumber, query.PageSize, cancellationToken);

        return pagedList;
    }
}