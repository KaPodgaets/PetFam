using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFam.BreedManagement.Infrastructure.Contexts;
using PetFam.Shared.Options;

namespace PetFam.BreedManagement.Infrastructure.BackgroundServices;

public class BreedManagementEntityCleaner : BackgroundService
{
    private readonly ILogger<BreedManagementEntityCleaner> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<SoftDeleteOptions> _softDeleteOptions;

    public BreedManagementEntityCleaner(
        ILogger<BreedManagementEntityCleaner> logger,
        IServiceProvider serviceProvider,
        IOptions<SoftDeleteOptions> softDeleteOptions)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _softDeleteOptions = softDeleteOptions;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("BreedManagement deleted entity cleaner started to work");
            await using var scope = _serviceProvider.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

            var entitiesToDelete = await dbContext.Species
                .Where(x => x.IsDeleted
                            && x.DeletedOn > DateTime.UtcNow.AddDays(_softDeleteOptions.Value.DaysBeforeRemove))
                .Include(x => x.Breeds)
                .ToListAsync(stoppingToken);

            dbContext.RemoveRange(entitiesToDelete);
            _logger.LogInformation("BreedManagement deleted entity cleaner ended to work");

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }

        await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        // Perform any cleanup actions if required
        await base.StopAsync(stoppingToken);
    }
}