using LedMatrixController.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LedMatrixController.BackgroundServices
{
    public class LedMatrixHostedService : BackgroundService
    {
        private readonly ILogger<LedMatrixHostedService> _logger;
        private readonly IImageService _imageService;
        private readonly ILedMatrixService _ledMatrixService;
        private readonly ILedMatrixLayoutService _ledMatrixLayoutService;
        public LedMatrixHostedService(
            ILogger<LedMatrixHostedService> logger,
            IImageService imageService,
            ILedMatrixService ledMatrixService,
            ILedMatrixLayoutService ledMatrixLayoutService
            )
        {
            _logger = logger ?? throw new NotImplementedException(logger.GetType().Name);
            _imageService = imageService ?? throw new NotImplementedException(imageService.GetType().Name);
            _ledMatrixService = ledMatrixService ?? throw new NotImplementedException(ledMatrixService.GetType().Name);
            _ledMatrixLayoutService = ledMatrixLayoutService ?? throw new NotImplementedException(ledMatrixLayoutService.GetType().Name);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("LedMatrixHostedService started");
            return base.StartAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ledMatrixService.Clear();
            while (!stoppingToken.IsCancellationRequested)
            {
                var sections = _ledMatrixLayoutService.GetLayoutSections();
                foreach (var section in sections)
                {
                    try
                    {
                        var pixelMatrix = section.GetSectionPixelMatrix();
                        if (pixelMatrix != null)
                        {
                            switch (section.Position)
                            {
                                case SectionPosition.Left:
                                    _ledMatrixService.SetDataMatrix(pixelMatrix, 0, 0);
                                    break;
                                case SectionPosition.Center:
                                    _ledMatrixService.SetDataMatrix(pixelMatrix, 8, 0);
                                    break;
                                case SectionPosition.Right:
                                    _ledMatrixService.SetDataMatrix(pixelMatrix, 24, 0);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Problem in section {section.GetType().FullName}");
                        // remove current section
                        _ledMatrixLayoutService.RemoveSection(section);
                    }
                }

                Thread.Sleep(50);
            }

            _ledMatrixService.Clear();
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogWarning("LedMatrixHostedService stopped");

            return base.StopAsync(cancellationToken);
        }
    }
}
