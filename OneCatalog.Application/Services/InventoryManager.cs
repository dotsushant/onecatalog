using System.Collections.Generic;
using OneCatalog.Application.Interfaces.Repositories;
using OneCatalog.Application.Interfaces.Services;
using OneCatalog.Domain.Models;

namespace OneCatalog.Application.Services
{
    public class InventoryManager : IInventoryManager
    {
        private readonly IProductRepository _productRepository;
        private readonly ISupplierProductBarcodeRepository _supplierProductBarcodeRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ITenantRepository _tenantRepository;

        public InventoryManager(IProductRepository productRepository, ISupplierRepository supplierRepository,
            ISupplierProductBarcodeRepository supplierProductBarcodeRepository,
            ITenantRepository tenantRepository)
        {
            _productRepository = productRepository;
            _supplierRepository = supplierRepository;
            _supplierProductBarcodeRepository = supplierProductBarcodeRepository;
            _tenantRepository = tenantRepository;
        }

        public void Initialize()
        {
            _productRepository.Load();
            _supplierRepository.Load();
            _supplierProductBarcodeRepository.Load();
        }

        public Product AddProduct(string tenantId, string sku, string description)
        {
            return _productRepository.Add(tenantId, sku, description);
        }

        public void RemoveProduct(string tenantId, string sku)
        {
            _productRepository.Remove(tenantId, sku);
        }

        public Product GetProductBySku(string tenantId, string sku)
        {
            return _productRepository.GetBySku(tenantId, sku);
        }

        public Product GetProductByBarcode(string tenantId, string barcode)
        {
            return _productRepository.GetByBarcode(tenantId, barcode);
        }

        public IEnumerable<Product> GetAllProducts(string tenantId)
        {
            return _productRepository.GetAll(tenantId);
        }

        public Supplier AddSupplier(string tenantId, int id, string name)
        {
            return _supplierRepository.Add(tenantId, id, name);
        }

        public IEnumerable<Supplier> GetAllSuppliers(string tenantId)
        {
            return _supplierRepository.GetAll(tenantId);
        }

        public void SaveProducts(string tenantId)
        {
            _productRepository.Save(tenantId);
        }

        public void SaveSuppliers(string tenantId)
        {
            _supplierRepository.Save(tenantId);
        }

        public void SaveSupplierProductBarcodes(string tenantId)
        {
            _supplierProductBarcodeRepository.Save(tenantId);
        }
    }
}