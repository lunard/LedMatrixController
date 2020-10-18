using System.Drawing;

namespace LedMatrixController.Services.Interfaces
{
    public interface IImageService
    {
        /// <summary>
        /// Converts a bitmap image to a matrix, where each value is the HTML color of that pixel 
        /// </summary>
        /// <param name="bmpImageFilePath"></param>
        /// <returns></returns>
        Color[,] ConvertImageToHtmlColorMatrix(string bmpImageFilePath);
    }
}
