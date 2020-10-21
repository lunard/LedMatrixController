using Iot.Device.Graphics;
using Iot.Device.Ws28xx;
using LedMatrixController.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Device.Spi;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrixController.Services.Impl
{
    public class Ws2812b_LedMatrixService : ILedMatrixService
    {
        private readonly ILogger<Ws2812b_LedMatrixService> _logger;

        private BitmapImage _matrixImage;
        private Ws2812b _matrixDevice;

        public Ws2812b_LedMatrixService(
            ILogger<Ws2812b_LedMatrixService> logger)
        {

            _logger = logger ?? throw new NotImplementedException(logger.GetType().Name);

            try
            {
                var settings = new SpiConnectionSettings(0, 0)
                {
                    ClockFrequency = 2_400_000,
                    Mode = SpiMode.Mode0,
                    DataBitLength = 8
                };

                var spi = SpiDevice.Create(settings);
                _matrixDevice = new Ws2812b(spi, 32, 8);
                _matrixImage = _matrixDevice.Image;

                _logger.LogInformation("Image:  {width}x{height}", _matrixImage.Width, _matrixImage.Height);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ws2812b_LedMatrixService: cannot create the SpiDevice");
            }
        }

        public void Clear(Color color = default)
        {
            _matrixImage.Clear(color);

            _matrixDevice.Update();
        }

        public void SetPixel(int x, int y, Color color)
        {
            _matrixImage.SetPixel(y % 2 == 0 ? x : 31 - x, y, color);

            _matrixDevice.Update();
        }

        public void SetRow(int y, Color color)
        {
            for (int i = 0; i < _matrixImage.Width; i++)
            {
                _matrixImage.SetPixel(i, y, color);
            }

            _matrixDevice.Update();
        }

        public void SetDataMatrix(Color[,] matrix, int xOffset, int yOffset)
        {
            if (xOffset + matrix.GetUpperBound(0) > 31 || yOffset + matrix.GetUpperBound(1) > 31)
            {
                _logger.LogError($"Cannot set a matrix {matrix.GetUpperBound(0)}x{matrix.GetUpperBound(1)} with a xOffset of {xOffset} and a yOffset of {yOffset}");
                throw new IndexOutOfRangeException();
            }

            _logger.LogInformation($"Set matrix {matrix.GetUpperBound(0)}x{matrix.GetUpperBound(1)} with a xOffset of {xOffset} and a yOffset of {yOffset}");

            for (int y = 0; y <= matrix.GetUpperBound(1); y++)
            {
                int setY = y + yOffset;
                for (int x = 0; x <= matrix.GetUpperBound(0); x++)
                {
                    int setX = x + xOffset;
                    _matrixImage.SetPixel(setY % 2 == 0 ? setX : 31 - setX, setY, matrix[x, y]);
                }
            }

            _matrixDevice.Update();
        }
    }
}
