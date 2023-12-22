namespace OneCatalog.Application.Interfaces.Repositories
{
    public interface ISupplierProductBarcodeRepository
    {
        void Load();
        void Save(string tenantId);
    }
}