using Microsoft.EntityFrameworkCore;
using PetFam.Shared.SharedKernel.Errors;
using PetFam.Shared.SharedKernel.Result;
using PetFam.VolunteeringApplications.Application.Database;
using PetFam.VolunteeringApplications.Domain;
using PetFam.VolunteeringApplications.Infrastructure.DbContexts;

namespace PetFam.VolunteeringApplications.Infrastructure;

public class ApplicationsRepository : IApplicationsRepository
{
    private readonly ApplicationsWriteDbContext _dbContext;

    public ApplicationsRepository(ApplicationsWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>> Add(
        VolunteeringApplication model,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Applications.AddAsync(model, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return Errors.General.Failure().ToErrorList();
        }

        return model.Id.Value;
    }

    public async Task<Result<VolunteeringApplication>> GetById(
        VolunteeringApplicationId id,
        CancellationToken cancellationToken = default)
    {
        var model = await _dbContext.Applications
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (model == null)
        {
            return Errors.General.NotFound(id.Value).ToErrorList();
        }
        return model;
    }

    public async Task<Result<Guid>> Update(
        VolunteeringApplication model,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Applications.Attach(model);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return model.Id.Value;
    }

    public async Task<Result<Guid>> Delete(
        VolunteeringApplication model,
        CancellationToken cancellationToken = default)
    {
        _dbContext.Applications.Remove(model);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return model.Id.Value;
    }

    public async Task<Result<IReadOnlyList<VolunteeringApplication>>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var models = await _dbContext.Applications
            .ToListAsync(cancellationToken);

        return models;
    }
}