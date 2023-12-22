using System;
using System.Collections.Generic;
using System.Linq;
using OneCatalog.Domain.Exceptions;
using OneCatalog.Helpers;

namespace OneCatalog.Domain.Models
{
    public class Supplier
    {
        private readonly HashSet<string> _barcodes = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        public Supplier(int id, string name)
        {
            Contract.Requires<SupplierException>(!string.IsNullOrWhiteSpace(name), Resource.SupplierNameIsInvalid);
            Id = id;
            Name = name;
        }

        public int Id { get; }

        public string Name { get; }

        public IEnumerable<string> Barcodes => _barcodes;

        public void AddBarcode(string barcode)
        {
            Contract.Requires<SupplierException>(!string.IsNullOrWhiteSpace(barcode), Resource.BarcodeIsInvalid);
            _barcodes.Add(barcode);
        }

        public void AddBarcodes(IEnumerable<string> barcodes)
        {
            Contract.Requires<SupplierException>(
                barcodes.Any() && barcodes.All(barcode => !string.IsNullOrWhiteSpace(barcode)),
                Resource.OneOrMoreBarcodesIsInvalid);

            _barcodes.UnionWith(barcodes);
        }
    }
}