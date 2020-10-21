using LedMatrixController.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;
using System.IO;

namespace LedMatrixController.Services.Impl
{
    public class ImageService : IImageService
    {
        ILogger<ImageService> _logger;
        public ImageService(ILogger<ImageService> logger)
        {
            _logger = logger ?? throw new NotImplementedException(logger.GetType().Name);
        }

        public Color[,] ConvertImageToHtmlColorMatrix(string bmpImageFilePath)
        {
            if (!File.Exists(bmpImageFilePath))
            {
                _logger.LogError("File {path} not found", bmpImageFilePath);
                throw new FileNotFoundException($"File {bmpImageFilePath} not found");
            }

            Bitmap image = (Bitmap)Image.FromFile(bmpImageFilePath);

            return ImageTo2DByteArray(image);
        }

        public Color[,] ImageTo2DByteArray(Bitmap bmp)
        {
            int width = bmp.Width;
            int height = bmp.Height;
            Color[,] result = new Color[width, height];

            for (int y = 0; y < height; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    result[y, x] = bmp.GetPixel(x, y);
                }
            }

            return result;
        }
    }
}
