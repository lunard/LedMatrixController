using System.Drawing;

namespace LedMatrixController.Services.Interfaces
{
    public enum SectionPosition
    {
        Left,
        Center,
        Right
    }

    public interface ISection
    {
        public SectionPosition Position { get; }

        Color[,] GetSectionPixelMatrix();
    }
}