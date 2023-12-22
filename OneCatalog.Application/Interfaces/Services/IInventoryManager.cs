using System.Collections.Generic;
using OneCatalog.Domain.Models;

namespace OneCatalog.Application.Interfaces.Services
{
    public interface IInventoryManager
    {
        void Initialize();
        Product AddProduct(string tenantId, string sku, string description);
        Product GetProductBySku(string tenantId, string sku);
        Product GetProductByBarcode(string tenantId, string barcode);
        IEnumerable<Product> GetAllProducts(string tenantId);
        void RemoveProduct(string tenantId, string sku);
        Supplier AddSupplier(string tenantId, int id, string name);
        IEnumerable<Supplier> GetAllSuppliers(string tenantId);
        void SaveProducts(string tenantId);
        void SaveSuppliers(string tenantId);
        void SaveSupplierProductBarcodes(string tenantId);
    }
}