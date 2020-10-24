namespace LedMatrixController.Services.Interfaces
{

    public interface ILedMatrixLayoutService
    {
        void SetSection(ISection section);
        void RemoveSection(ISection section);
        ISection[] GetLayoutSections();
    }
}
