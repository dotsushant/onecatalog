using System;
using System.Collections.Generic;
using System.Linq;
using OneCatalog.Application.Interfaces.Repositories;
using OneCatalog.Application.Interfaces.Services;
using OneCatalog.Domain.Helpers;

namespace OneCatalog.Application.Services
{
    public class CatalogManager : ICatalogManager
    {
        private readonly IInventoryManager _inventoryManager;
        private readonly IReportGenerator _reportGenerator;
        private readonly ITenantRepository _tenantRepository;

        public CatalogManager(IInventoryManager inventoryManager,
            ITenantRepository tenantRepository, IReportGenerator reportGenerator)
        {
            _inventoryManager = inventoryManager;
            _tenantRepository = tenantRepository;
            _reportGenerator = reportGenerator;
        }

        public void Consolidate()
        {
            _inventoryManager.Initialize();
            var acquirerBarcodes = _inventoryManager.GetAllSuppliers(_tenantRepository.Acquirer.Id)
                .SelectMany(r => r.Barcodes);
            var acquireeBarcodes = _inventoryManager.GetAllSuppliers(_tenantRepository.Acquiree.Id)
                .SelectMany(r => r.Barcodes);
            var acquireeBarcodesToMerge = acquireeBarcodes.Except(acquirerBarcodes, StringComparer.OrdinalIgnoreCase);
            var acquirerProducts = _inventoryManager.GetAllProducts(_tenantRepository.Acquirer.Id);
            var acquireeProducts = acquireeBarcodesToMerge
                .Select(barcode => _inventoryManager.GetProductByBarcode(_tenantRepository.Acquiree.Id, barcode))
                .Distinct(new ProductEqualityComparer());
            _reportGenerator.GenerateReport(new ReportData(acquirerProducts, acquireeProducts));
        }

        public void AddProduct(string sku, string description)
        {
            _inventoryManager.Initialize();
            _inventoryManager.AddProduct(_tenantRepository.Acquirer.Id, sku, description);
            _inventoryManager.SaveProducts(_tenantRepository.Acquirer.Id);
        }

        public void RemoveProduct(string sku)
        {
            _inventoryManager.Initialize();
            var tenantId = _tenantRepository.Acquirer.Id;
            _inventoryManager.RemoveProduct(tenantId, sku);
            _inventoryManager.SaveProducts(tenantId);
            _inventoryManager.SaveSupplierProductBarcodes(tenantId);
        }

        public void AddSupplier(string productSku, string supplierName, IEnumerable<string> productBarcodes)
        {
            _inventoryManager.Initialize();
            var tenantId = _tenantRepository.Acquiree.Id;
            var newSupplierId =
                _inventoryManager.GetAllSuppliers(tenantId).Count() + 1; // Simple id generator logic
            var supplier = _inventoryManager.AddSupplier(tenantId, newSupplierId, supplierName);
            supplier?.AddBarcodes(productBarcodes);
            var product = _inventoryManager.GetProductBySku(tenantId, productSku);
            product?.AddSupplier(supplier);
            _inventoryManager.SaveProducts(tenantId);
            _inventoryManager.SaveSuppliers(tenantId);
            _inventoryManager.SaveSupplierProductBarcodes(tenantId);
        }
    }
}