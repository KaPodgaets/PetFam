using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.Accounts.Application.Database;
using PetFam.Accounts.Application.DataModels;
using PetFam.Accounts.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Accounts.Application.Features.GetUserById;

public class GetUserByIdHandler
    :IQueryHandler<UserDataModel, GetUserByIdQuery>
{
    private readonly IAccountsReadDbContext _dbContext;
    private readonly ILogger<GetUserByIdHandler> _logger;
    public GetUserByIdHandler(
        IAccountsReadDbContext dbContext, 
        ILogger<GetUserByIdHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    public async Task<Result<UserDataModel>> HandleAsync(
        GetUserByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Id == query.UserId, cancellationToken: cancellationToken);

        if (user is null)
            return Errors.General.NotFound("account").ToErrorList();

        return user;
    }
}