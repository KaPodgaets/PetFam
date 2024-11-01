using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFam.Application.FileManagement;
using PetFam.Application.FileProvider;
using PetFam.Application.VolunteerManagement;
using PetFam.Infrastructure.Options;

namespace PetFam.Infrastructure.BackgroundServices
{

    public class FilesSynchronizerService : BackgroundService
    {
        private readonly ILogger<FilesSynchronizerService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IFilesCleanerMessageQueue _queue;


        public FilesSynchronizerService(
            ILogger<FilesSynchronizerService> logger,
            IServiceProvider serviceProvider,
            IFilesCleanerMessageQueue queue)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("FilesSynchronizerService started to work");
                //await using var scope = _serviceProvider.CreateAsyncScope();

                //var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();

                //var photoFilePathsResult = await fileProvider.GetFiles(MinioOptions.PHOTO_BUCKET);
                //var photoFilePaths = photoFilePathsResult.Value;

                //var volunteerRepository = scope.ServiceProvider.GetRequiredService<IVolunteerRepository>();

                //var volunteersResult = await volunteerRepository.GetAllAsync(stoppingToken);
                //var volunteers = volunteersResult.Value;
                //var photoPaths = volunteers
                //    .SelectMany(v => v.Pets)
                //    .Select(p => p.Gallery)
                //    .SelectMany(g => g.Value)
                //    .Select(ph => ph.FilePath)
                //    .ToList();

                //var result = photoFilePaths.Except(photoPaths).ToList();

                //// pass result into message queue
                //await _queue.WriteAsync(result.ToArray(), stoppingToken);
                
                _logger.LogInformation("FilesSynchronizerService ended to work");

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
}
