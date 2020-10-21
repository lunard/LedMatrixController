using LedMatrixController.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LedMatrixController.Services.Impl
{
    public class LedMatrixLayoutService : ILedMatrixLayoutService
    {
        Dictionary<SectionPosition, ISection> sections = new Dictionary<SectionPosition, ISection>();
        public ISection[] GetLayoutSections()
        {
            return sections.Values.ToArray();
        }

        public void SetLayoutSection(ISection section)
        {
            sections[section.Position] = section;
        }
    }
}
