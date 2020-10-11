using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Device.Spi;

namespace LedMatrixController.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private BitmapImage matrixImage;
        private Ws2812b matrixDevice;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger ?? throw new NotImplementedException(logger.GetType().Name);

            var settings = new SpiConnectionSettings(0, 0)
            {
                ClockFrequency = 2_400_000,
                Mode = SpiMode.Mode0,
                DataBitLength = 8
            };

            var spi = SpiDevice.Create(settings);
            matrixDevice = new Ws2812b(spi, 32, 8);
            matrixImage = matrixDevice.Image;
            matrixImage.Clear();
            _logger.LogInformation("Image:  {width}x{height}", matrixImage.Width, matrixImage.Height);
        }

        [Route("led/{x}/{y}/{htmlColor}")]
        public IActionResult SetLED(int x, int y, string htmlColor)
        {
            try
            {
                matrixImage.SetPixel(y % 2 == 0 ? x : 31 - x, y, System.Drawing.ColorTranslator.FromHtml(htmlColor));
                matrixDevice.Update();

                _logger.LogInformation($"Set pixel {x}x{y} to color {htmlColor}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetLED");
                return BadRequest();
            }
        }

        [Route("row/{index}/{htmlColor}")]
        public IActionResult SetLEDRow(int index, string htmlColor)
        {
            try
            {
                for (int i = 0; i < matrixImage.Width; i++)
                {
                    matrixImage.SetPixel(i, index, System.Drawing.ColorTranslator.FromHtml(htmlColor));
                }

                matrixDevice.Update();

                _logger.LogInformation($"Set row {index} to color {htmlColor}");

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SetLEDRow");
                return BadRequest();
            }
        }

        [Route("led/reset/all")]
        public IActionResult ResetAll()
        {
            try
            {
                matrixDevice.Update();

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
