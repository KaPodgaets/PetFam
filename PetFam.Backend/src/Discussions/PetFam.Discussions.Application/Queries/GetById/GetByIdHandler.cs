using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PetFam.Discussions.Application.Database;
using PetFam.Discussions.Domain;
using PetFam.Shared.Abstractions;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Discussions.Application.Queries.GetById;

public class GetByIdHandler:IQueryHandler<Discussion, GetByIdQuery>
{
    private readonly IDiscussionsReadDbContext _context;
    
    public GetByIdHandler(IDiscussionsReadDbContext context)
    {
        _context = context;
    }
    public async Task<Result<Discussion>> HandleAsync(
        GetByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        if (query.Id == Guid.Empty)
            return Errors.General.ValueIsInvalid("Id").ToErrorList();
        var discussion = await _context.Discussions
            .FirstOrDefaultAsync(x => x.Id == DiscussionId.Create(query.Id), cancellationToken);
        if (discussion is null)
            return Errors.General.NotFound("Discussion Not Found").ToErrorList();

        return discussion;
    }
}