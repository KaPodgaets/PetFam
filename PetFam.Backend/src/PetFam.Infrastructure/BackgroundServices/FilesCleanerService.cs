using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFam.Application.FileManagement;
using PetFam.Infrastructure.Options;
using PetFam.Shared.Messaging;
using PetFam.Shared.Shared;
using PetFam.Volunteers.Application.FileProvider;

namespace PetFam.Infrastructure.BackgroundServices
{
    public class FilesCleanerService : BackgroundService
    {
        private readonly ILogger<FilesCleanerService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageQueue _queue;


        public FilesCleanerService(
            ILogger<FilesCleanerService> logger,
            IServiceProvider serviceProvider,
            IMessageQueue queue)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("FilesCleanerService started to work");
                await using var scope = _serviceProvider.CreateAsyncScope();

                var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();

                var paths = await _queue.ReadAsync(stoppingToken);

                foreach ( var path in paths)
                {
                    var fileMetadata = new FileMetadata(Constants.FileManagementOptions.PHOTO_BUCKET, path);
                    var result = await fileProvider.DeleteFile(fileMetadata, stoppingToken);

                    if (result.IsFailure)
                    {
                        _logger.LogError("unable delete photo {path}", paths);
                    }
                }
                
                await Task.Delay(3000, stoppingToken);

                _logger.LogInformation("FilesCleanerService ended to work");
            }

            await Task.CompletedTask;
        }
    }
}
