using System.Collections.Generic;
using System.Linq;
using OneCatalog.Domain.Models;
using OneCatalog.Infrastructure.Records;

namespace OneCatalog.Infrastructure.Extensions
{
    public static class RecordExtensions
    {
        public static ProductRecord ToProductRecord(this Product product)
        {
            return new ProductRecord
            {
                Sku = product.Sku,
                Description = product.Description
            };
        }

        public static SupplierRecord ToSupplierRecord(this Supplier supplier)
        {
            return new SupplierRecord
            {
                Id = supplier.Id,
                Name = supplier.Name
            };
        }

        public static IEnumerable<ProductSourceRecord> ToProductSourceRecords(this IEnumerable<Product> products,
            string source)
        {
            return products.Select(product => new ProductSourceRecord
            {
                Sku = product.Sku,
                Description = product.Description,
                Source = source
            });
        }
    }
}