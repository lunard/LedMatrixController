using LedMatrixController.Services.Interfaces;
using System.Drawing;

namespace LedMatrixController.Services.Impl
{
    public abstract class SectionBase : ISection
    {
        private SectionPosition _position;

        public SectionBase(SectionPosition position)
        {
            _position = position;
        }

        public SectionPosition Position
        {
            get { return _position; }
        }

        public abstract Color[,] GetSectionPixelMatrix();
    }
}
