using FluentValidation;
using Microsoft.Extensions.Logging;
using PetFam.PetManagement.Application.Database;
using PetFam.Shared.Abstractions;
using PetFam.Shared.Dtos;
using PetFam.Shared.Extensions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.PetManagement.Application.VolunteerManagement.Queries.GetPetById;

public class GetPetByIdHandler
    :IQueryHandler<PetDto,GetPetByIdQuery>
{
    private readonly IReadDbContext _dbContext;
    private readonly ILogger<GetPetByIdHandler> _logger;
    private readonly IValidator<GetPetByIdQuery> _validator;
    
    public GetPetByIdHandler(
        IReadDbContext dbContext,
        ILogger<GetPetByIdHandler> logger,
        IValidator<GetPetByIdQuery> validator)
    {
        _dbContext = dbContext;
        _logger = logger;
        _validator = validator;
    }
    public async Task<Result<PetDto>> HandleAsync(
        GetPetByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        // validate
        var validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (validationResult.IsValid is false)
            return validationResult.ToErrorList();
        
        // get dto

        var petDto = _dbContext.Pets
            .FirstOrDefault(p => p.Id == query.Id);

        if (petDto is null)
            return Errors.General.NotFound($"petId {query.Id}").ToErrorList();

        // return dto
        return petDto;
    }
}