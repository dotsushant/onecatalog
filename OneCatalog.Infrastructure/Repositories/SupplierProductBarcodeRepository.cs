using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using OneCatalog.Application;
using OneCatalog.Application.Interfaces.Repositories;
using OneCatalog.Helpers;
using OneCatalog.Infrastructure.Records;

namespace OneCatalog.Infrastructure.Repositories
{
    public class SupplierProductBarcodeRepository : ISupplierProductBarcodeRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IProductRepository _productRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ITenantRepository _tenantRepository;

        public SupplierProductBarcodeRepository(IProductRepository productRepository,
            ISupplierRepository supplierRepository, IConfiguration configuration, ITenantRepository tenantRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _configuration = configuration;
            _tenantRepository = tenantRepository;
        }

        public void Load()
        {
            LoadInitialData(_tenantRepository.Acquirer.Id);
            LoadInitialData(_tenantRepository.Acquiree.Id);
        }

        public void Save(string tenantId)
        {
            var datasetLocation = _configuration[Constants.Configuration.DatasetLocation];
            var supplierProductsBarcodesDataSource =
                _tenantRepository.GetById(tenantId)?.Config.SupplierProductBarcodesDataSource;

            var supplierProductBarcodeRecords = _productRepository.GetAll(tenantId).SelectMany(product =>
                product.Suppliers.SelectMany(supplier =>
                    supplier.Barcodes.Select(barcode => new SupplierProductBarcodeRecord
                    {
                        ProductSku = product.Sku,
                        SupplierId = supplier.Id,
                        ProductBarcode = barcode
                    })));

            CsvHelper.WriteRecords(Path.Combine(datasetLocation, supplierProductsBarcodesDataSource),
                supplierProductBarcodeRecords.OrderBy(p => p.SupplierId));
        }

        private void LoadInitialData(string tenantId)
        {
            var supplierProductBarcodesDataSource = Path.Combine(
                _configuration[Constants.Configuration.DatasetLocation],
                _configuration[$"{tenantId}:{Constants.Configuration.SupplierProductBarcodesDataSource}"]);

            var supplierProductBarcodesRecords =
                CsvHelper.ReadRecords<SupplierProductBarcodeRecord>(supplierProductBarcodesDataSource);

            supplierProductBarcodesRecords.ForEach(supplierProductBarcodesRecord =>
            {
                var product = _productRepository.GetBySku(tenantId, supplierProductBarcodesRecord.ProductSku);
                var supplier = _supplierRepository.GetById(tenantId, supplierProductBarcodesRecord.SupplierId);
                product?.AddSupplier(supplier);
                supplier?.AddBarcode(supplierProductBarcodesRecord.ProductBarcode);
            });
        }
    }
}