using System;
using System.Collections.Generic;
using OneCatalog.Domain.Models;

namespace OneCatalog.Domain.Helpers
{
    public class ProductEqualityComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            return string.Equals(x.Sku, y.Sku, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Product product)
        {
            return product.Sku.GetHashCode();
        }
    }
}