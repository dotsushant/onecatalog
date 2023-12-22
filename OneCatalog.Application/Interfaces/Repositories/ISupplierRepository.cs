using System.Collections.Generic;
using OneCatalog.Domain.Models;

namespace OneCatalog.Application.Interfaces.Repositories
{
    public interface ISupplierRepository
    {
        Supplier Add(string tenantId, int id, string name);
        Supplier GetById(string tenantId, int id);
        IEnumerable<Supplier> GetAll(string tenantId);
        IEnumerable<Supplier> GetAllByBarcode(string tenantId, string barcode);
        void Load();
        void Save(string tenantId);
    }
}