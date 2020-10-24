using LedMatrixController.Services.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace LedMatrixController.Services.Impl.Sections
{
    public class SectionAnimation : SectionBase
    {
        private readonly TimeSpan _transitionDuration;
        private readonly string _firstImageName;
        private readonly string _imageBasePath;
        private readonly int _numberAnimationImages;

        private readonly IImageService _imageService;
        private int _imageCounter = 1;
        private DateTime _lastUpdate = DateTime.MinValue;

        public SectionAnimation(
            SectionPosition position,
            string firstImageName,
            string imageBasePath,
            TimeSpan transitionDuration,
            IImageService imageService
            ) : base(position)
        {
            _imageService = imageService ?? throw new NotImplementedException(imageService.GetType().Name);

            _transitionDuration = transitionDuration;
            _imageBasePath = imageBasePath;
            _firstImageName = firstImageName;
            var partialName = _firstImageName.Split("1")[0];
            _numberAnimationImages = Directory.EnumerateFiles(_imageBasePath, partialName + "*", SearchOption.TopDirectoryOnly).Count();
        }

        public override Color[,] GetSectionPixelMatrix()
        {
            if ((DateTime.Now - _lastUpdate) > _transitionDuration)
            {
                if (_imageCounter > _numberAnimationImages)
                    _imageCounter = 1;

                var imagePath = Path.Combine(_imageBasePath, _firstImageName.Replace("1", _imageCounter.ToString()));

                _lastUpdate = DateTime.Now;
                _imageCounter++;
                return _imageService.ConvertImageToHtmlColorMatrix(imagePath);
                
            }
            else
                return null;
        }
    }
}
