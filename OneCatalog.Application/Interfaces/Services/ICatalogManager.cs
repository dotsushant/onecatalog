using System.Collections.Generic;

namespace OneCatalog.Application.Interfaces.Services
{
    public interface ICatalogManager
    {
        void Consolidate();

        void AddProduct(string sku, string description);

        void RemoveProduct(string sku);

        void AddSupplier(string productSku, string supplierName, IEnumerable<string> productBarcodes);
    }
}