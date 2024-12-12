using Microsoft.EntityFrameworkCore;
using PetFam.Discussions.Application;
using PetFam.Discussions.Domain;
using PetFam.Discussions.Infrastructure.DbContexts;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;

namespace PetFam.Discussions.Infrastructure;

public class DiscussionsRepository : IDiscussionsRepository
{
    private readonly DiscussionsWriteDbContext _dbContext;

    public DiscussionsRepository(DiscussionsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>> Add(Discussion model, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Discussions.AddAsync(model, cancellationToken);
        }
        catch (Exception)
        {
            return Errors.General.Failure().ToErrorList();
        }

        return model.Id.Value;
    }

    public async Task<Result<Discussion>> GetById(DiscussionId id, CancellationToken cancellationToken = default)
    {
        var model = await _dbContext.Discussions
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (model == null)
        {
            return Errors.General.NotFound(id.Value).ToErrorList();
        }

        return model;
    }

    public Result<Guid> Update(Discussion volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Discussions.Attach(volunteer);
        return volunteer.Id.Value;
    }

    public Result<Guid> Delete(Discussion volunteer, CancellationToken cancellationToken = default)
    {
        _dbContext.Discussions.Remove(volunteer);
        return volunteer.Id.Value;
    }

    public async Task<Result<IReadOnlyList<Discussion>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var models = await _dbContext.Discussions
            .ToListAsync(cancellationToken);

        return models;
    }
}