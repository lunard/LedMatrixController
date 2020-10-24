using LedMatrixController.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LedMatrixController.Services.Impl
{
    public class LedMatrixLayoutService : ILedMatrixLayoutService
    {
        ILogger<LedMatrixLayoutService> _logger;

        public LedMatrixLayoutService(
            ILogger<LedMatrixLayoutService> logger)
        {
            _logger = logger ?? throw new NotImplementedException(logger.GetType().Name);
        }

        Dictionary<SectionPosition, ISection> sections = new Dictionary<SectionPosition, ISection>();
        public ISection[] GetLayoutSections()
        {
            return sections.Values.ToArray();
        }

        public void SetSection(ISection section)
        {
            sections[section.Position] = section;
            _logger.LogInformation("Section added: {@section}", section);
        }
        public void RemoveSection(ISection section)
        {
            sections.Remove(section.Position);
            _logger.LogWarning("Section removed: {@section}", section);
        }
    }
}
