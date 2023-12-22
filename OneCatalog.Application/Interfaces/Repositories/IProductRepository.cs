using System.Collections.Generic;
using OneCatalog.Domain.Models;

namespace OneCatalog.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Product Add(string tenantId, string sku, string description);
        void Remove(string tenantId, string sku);
        Product GetBySku(string tenantId, string sku);
        Product GetByBarcode(string tenantId, string barcode);
        IEnumerable<Product> GetAll(string tenantId);
        void Load();
        void Save(string tenantId);
    }
}