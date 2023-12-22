using System.Collections.Generic;
using OneCatalog.Domain.Models;

namespace OneCatalog.Domain.Helpers
{
    public class SupplierEqualityComparer : IEqualityComparer<Supplier>
    {
        public bool Equals(Supplier x, Supplier y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Supplier supplier)
        {
            return supplier.Id.GetHashCode();
        }
    }
}