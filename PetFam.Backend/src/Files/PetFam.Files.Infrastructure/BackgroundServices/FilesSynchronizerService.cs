﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PetFam.Files.Application.FileProvider;
using PetFam.PetManagement.Contracts;
using PetFam.Shared.Messaging;
using PetFam.Shared.SharedKernel;

namespace PetFam.Files.Infrastructure.BackgroundServices
{

    public class FilesSynchronizerService : BackgroundService
    {
        private readonly ILogger<FilesSynchronizerService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageQueue _queue;


        public FilesSynchronizerService(
            ILogger<FilesSynchronizerService> logger,
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
                _logger.LogInformation("FilesSynchronizerService started to work");
                await using var scope = _serviceProvider.CreateAsyncScope();

                var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();
                var volunteerContracts = scope.ServiceProvider.GetRequiredService<IVolunteerContracts>();

                var photoFilePathsResult = await fileProvider
                    .GetFiles(Constants.FileManagementOptions.PHOTO_BUCKET);
                var photoFilePaths = photoFilePathsResult.Value;

                var photoPaths = await volunteerContracts.GetAllPhotoPaths(stoppingToken);
                var result = photoFilePaths.Except(photoPaths).ToList();

                // pass result into message queue
                await _queue.WriteAsync(result.ToArray(), stoppingToken);

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
