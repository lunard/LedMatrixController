using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LedMatrixController.Services.Interfaces
{
    public interface ILedMatrixService
    {
        //
        // Summary:
        //     Clears the image to specific color
        //
        // Parameters:
        //   color:
        //     Color to clear the image. Defaults to black.
        public void Clear(Color color = default);
        //
        // Summary:
        //     Sets pixel at specific position
        //
        // Parameters:
        //   x:
        //     X coordinate of the pixel
        //
        //   y:
        //     Y coordinate of the pixel
        //
        //   color:
        //     Color to set the pixel to
        public void SetPixel(int x, int y, Color color);

        public void SetRow(int y, Color color);

        public void SetDataMatrix(Color [,] matrix, int xOffset, int yOffset);
    }
}
