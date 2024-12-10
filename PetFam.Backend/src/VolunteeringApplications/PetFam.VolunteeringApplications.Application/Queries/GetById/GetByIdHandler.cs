using FluentValidation;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Domain;

namespace PetFam.VolunteeringApplications.Application.Queries.GetById;

public class GetByIdHandler:IQueryHandler<VolunteeringApplication, GetByIdQuery>
{
    private readonly IValidator<GetByIdQuery> _validator;
    private readonly IApplicationsReadDbContext _readDbContext;
    
    public GetByIdHandler(
        IValidator<GetByIdQuery> validator,
        IApplicationsReadDbContext readDbContext)
    {
        _validator = validator;
        _readDbContext = readDbContext;
    }
    public async Task<Result<VolunteeringApplication>> HandleAsync(GetByIdQuery query, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();
        
        var application = _readDbContext.Applications
            .FirstOrDefault(x => x.Id == VolunteeringApplicationId.Create(query.Id));

        if (application is null)
            return Errors.General.NotFound("Id").ToErrorList();

        return application;
    }
}