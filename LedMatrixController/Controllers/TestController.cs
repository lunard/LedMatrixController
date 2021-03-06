﻿using LedMatrixController.Services.Impl.Sections;
using LedMatrixController.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;

namespace LedMatrixController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IImageService _imageService;
        private readonly ILedMatrixService _ledMatrixService;
        private readonly ILedMatrixLayoutService _ledMatrixLayoutService;

        public TestController(
            ILogger<TestController> logger,
            IImageService imageService,
            ILedMatrixService ledMatrixService,
            ILedMatrixLayoutService ledMatrixLayoutService)
        {
            _logger = logger ?? throw new NotImplementedException(logger.GetType().Name);
            _imageService = imageService ?? throw new NotImplementedException(imageService.GetType().Name);
            _ledMatrixService = ledMatrixService ?? throw new NotImplementedException(ledMatrixService.GetType().Name);
            _ledMatrixLayoutService = ledMatrixLayoutService ?? throw new NotImplementedException(ledMatrixLayoutService.GetType().Name);
        }

        [Route("led/{x}/{y}/{hexColor}")]
        public IActionResult SetLED(int x, int y, string hexColor)
        {
            try
            {
                _ledMatrixService.SetPixel(x, y, System.Drawing.ColorTranslator.FromHtml("#" + hexColor));

                _logger.LogInformation($"Set pixel {x}x{y} to color {hexColor}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetLED");
                return BadRequest();
            }
        }

        [Route("row/{y}/{hexColor}")]
        public IActionResult SetLEDRow(int y, string hexColor)
        {
            try
            {
                _ledMatrixService.SetRow(y, System.Drawing.ColorTranslator.FromHtml("#" + hexColor));

                _logger.LogInformation($"Set row {y} to color {hexColor}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetLEDRow");
                return BadRequest();
            }
        }

        [Route("matrix/{xOffset}/{yOffset}")]
        public IActionResult SetLEDMatrix(int xOffset, int yOffset)
        {
            try
            {
                System.Drawing.Color[,] matrix = new System.Drawing.Color[,] { { System.Drawing.ColorTranslator.FromHtml("blue"), System.Drawing.ColorTranslator.FromHtml("red") }, { System.Drawing.ColorTranslator.FromHtml("red"), System.Drawing.ColorTranslator.FromHtml("blue") } };
                _ledMatrixService.SetDataMatrix(matrix, xOffset, yOffset);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetLEDMatrix");
                return BadRequest();
            }
        }

        [Route("image/{xOffset}/{yOffset}/{testImageId}")]
        public IActionResult SetLEDImage(int xOffset, int yOffset, int testImageId)
        {
            try
            {
                var bmpImagePath = Path.Combine(Directory.GetCurrentDirectory(), $"Images/stt_core_{testImageId}.bmp");
                var matrix = _imageService.ConvertImageToHtmlColorMatrix(bmpImagePath);
                _logger.LogInformation($"Image '{bmpImagePath}' converted in Color matrix with rank {matrix.Rank}");
                _ledMatrixService.SetDataMatrix(matrix, xOffset, yOffset);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetLEDMatrix");
                return BadRequest();
            }
        }


        [Route("image2/{testImageId}")]
        public IActionResult SetLEDImage2(int testImageId)
        {
            try
            {
                var bmpImagePath = Path.Combine(Directory.GetCurrentDirectory(), $"Images/stt_core_{testImageId}.bmp");
                var section = new SectionImage(SectionPosition.Left, bmpImagePath, _imageService);
                _ledMatrixLayoutService.SetSection(section);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetLEDMatrix");
                return BadRequest();
            }
        }


        [Route("animation/{firstImageName}/{transitionDuration}")]
        public IActionResult SetLEDAnimation(string firstImageName, int transitionDuration)
        {
            try
            {
                var imageBasePath = Path.Combine(Directory.GetCurrentDirectory(), $"Images/");
                var section = new SectionAnimation(SectionPosition.Center, firstImageName, imageBasePath, TimeSpan.FromMilliseconds(transitionDuration), _imageService);
                _ledMatrixLayoutService.SetSection(section);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetLEDMatrix");
                return BadRequest();
            }
        }

        [Route("led/reset/all")]
        public IActionResult ResetAll()
        {
            try
            {
                _ledMatrixService.Clear();

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ResetAll");
                return BadRequest();
            }
        }
    }
}
