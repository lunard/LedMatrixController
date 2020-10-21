using LedMatrixController.Services.Interfaces;
using System.Drawing;

namespace LedMatrixController.Services.Impl.Sections
{
    public class SectionImage : SectionBase
    {
        Color[,] _matrix;

        public SectionImage(
            SectionPosition position,
            string bmpImagePath,
            IImageService imageService
            ) : base(position)
        {
            _matrix = imageService.ConvertImageToHtmlColorMatrix(bmpImagePath);
        }

        public override Color[,] GetSectionPixelMatrix()
        {
            return _matrix;
        }
    }
}
