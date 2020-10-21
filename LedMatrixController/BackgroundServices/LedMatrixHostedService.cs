using LedMatrixController.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LedMatrixController.BackgroundServices
{
    public class LedMatrixHostedService : BackgroundService
    {
        private readonly ILogger<LedMatrixHostedService> _logger;
        private readonly IImageService _imageService;
        private readonly ILedMatrixService _ledMatrixService;

        public LedMatrixHostedService(
            ILogger<LedMatrixHostedService> logger,
            IImageService imageService,
            ILedMatrixService ledMatrixService
            )
        {
            _logger = logger ?? throw new NotImplementedException(logger.GetType().Name);
            _imageService = imageService ?? throw new NotImplementedException(imageService.GetType().Name);
            _ledMatrixService = ledMatrixService ?? throw new NotImplementedException(ledMatrixService.GetType().Name);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("LedMatrixHostedService started");
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("LedMatrixHostedService stopped");

            return base.StopAsync(cancellationToken);
        }
    }
}
