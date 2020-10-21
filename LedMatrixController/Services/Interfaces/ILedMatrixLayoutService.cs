namespace LedMatrixController.Services.Interfaces
{

    public interface ILedMatrixLayoutService
    {
        void SetLayoutSection(ISection section);
        ISection[] GetLayoutSections();
    }
}
