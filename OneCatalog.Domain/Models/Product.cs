using System;
using System.Collections.Generic;
using System.Linq;
using OneCatalog.Domain.Exceptions;
using OneCatalog.Domain.Helpers;
using OneCatalog.Helpers;

namespace OneCatalog.Domain.Models
{
    public class Product
    {
        private readonly HashSet<Supplier> _suppliers = new HashSet<Supplier>(new SupplierEqualityComparer());

        public Product(string sku, string description)
        {
            Contract.Requires<ProductException>(!string.IsNullOrWhiteSpace(sku), Resource.ProductSkuIsInvalid);
            Contract.Requires<ProductException>(!string.IsNullOrWhiteSpace(description),
                Resource.ProductDescriptionIsInvalid);

            Sku = sku;
            Description = description;
        }

        public string Sku { get; }

        public string Description { get; }

        public IEnumerable<Supplier> Suppliers => _suppliers;

        public bool HasBarcode(string barcode)
        {
            return _suppliers.Any(supplier => supplier.Barcodes.Contains(barcode, StringComparer.OrdinalIgnoreCase));
        }

        public void AddSupplier(Supplier supplier)
        {
            Contract.Requires<ProductException>(supplier != null, Resource.SupplierIsInvalid);
            _suppliers.Add(supplier);
        }
    }
}