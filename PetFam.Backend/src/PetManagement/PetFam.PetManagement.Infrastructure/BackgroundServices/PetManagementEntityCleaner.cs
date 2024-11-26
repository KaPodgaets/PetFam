using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetFam.PetManagement.Infrastructure.DbContexts;
using PetFam.Shared.Options;

namespace PetFam.PetManagement.Infrastructure.BackgroundServices;

public class PetManagementEntityCleaner:BackgroundService
{
    
    private readonly ILogger<PetManagementEntityCleaner> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IOptions<SoftDeleteOptions> _softDeleteOptions;

    public PetManagementEntityCleaner(
        ILogger<PetManagementEntityCleaner> logger,
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
            _logger.LogInformation("PetManagement deleted entity cleaner started to work");
            await using var scope = _serviceProvider.CreateAsyncScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();

            var entitiesToDelete = await dbContext.Volunteers
                .Where(x => x.IsDeleted 
                            && x.DeletedOn > DateTime.UtcNow.AddDays(_softDeleteOptions.Value.DaysBeforeRemove))
                .ToListAsync(stoppingToken);
            
            dbContext.RemoveRange(entitiesToDelete);
            _logger.LogInformation("PetManagement deleted entity cleaner ended to work");

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