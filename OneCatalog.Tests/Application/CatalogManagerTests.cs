using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using OneCatalog.Application;
using OneCatalog.Application.Interfaces.Repositories;
using OneCatalog.Application.Interfaces.Services;
using OneCatalog.Application.Services;
using OneCatalog.Domain.Models;

namespace OneCatalog.Tests.Application
{
    class CatalogManagerTests
    {
        private IInventoryManager _inventoryManager;
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<ISupplierRepository> _supplierRepositoryMock = new Mock<ISupplierRepository>();
        private readonly Mock<ISupplierProductBarcodeRepository> _supplierProductBarcodeRepositoryMock =
            new Mock<ISupplierProductBarcodeRepository>();
        private readonly Mock<IReportGenerator> _reportGeneratorMock = new Mock<IReportGenerator>();
        private readonly Mock<ITenantRepository> _tenantRepositoryMock = new Mock<ITenantRepository>();

        [TestCase]
        public void ShouldAddProductToTheAcquirerCatalog()
        {
            string sku = "999-vyk-317", description = "Cheese cake";

            _tenantRepositoryMock.Setup(m => m.Acquirer).Returns(new Tenant(Constants.Configuration.Acquirer,
                Constants.Configuration.Acquirer, new TenantConfig("p", "s", "ps")));

            _tenantRepositoryMock.Setup(m => m.Acquiree).Returns(new Tenant(Constants.Configuration.Acquiree,
                Constants.Configuration.Acquiree, new TenantConfig("p", "s", "ps")));

            _productRepositoryMock.Setup(m => m.GetBySku(Constants.Configuration.Acquirer, "999-vyk-317"))
                .Returns(new Product(sku, description));

            _inventoryManager = new InventoryManager(_productRepositoryMock.Object, _supplierRepositoryMock.Object,
                _supplierProductBarcodeRepositoryMock.Object, _tenantRepositoryMock.Object);

            var catalogManager = new CatalogManager(_inventoryManager, _tenantRepositoryMock.Object,
                _reportGeneratorMock.Object);

            catalogManager.AddProduct(sku, description);
            var product = _inventoryManager.GetProductBySku(Constants.Configuration.Acquirer, sku);
            Assert.AreEqual(product.Sku, sku);
            Assert.AreEqual(product.Description, description);
        }

        [TestCase]
        public void ShouldNotAddProductToTheAcquireeCatalog()
        {
            string sku = "999-vyk-317", description = "Cheese cake";

            _tenantRepositoryMock.Setup(m => m.Acquirer).Returns(new Tenant(Constants.Configuration.Acquirer,
                Constants.Configuration.Acquirer, new TenantConfig("p", "s", "ps")));

            _tenantRepositoryMock.Setup(m => m.Acquiree).Returns(new Tenant(Constants.Configuration.Acquiree,
                Constants.Configuration.Acquiree, new TenantConfig("p", "s", "ps")));

            _productRepositoryMock.Setup(m => m.GetBySku(Constants.Configuration.Acquirer, "999-vyk-317"))
                .Returns(new Product(sku, description));

            _inventoryManager = new InventoryManager(_productRepositoryMock.Object, _supplierRepositoryMock.Object,
                _supplierProductBarcodeRepositoryMock.Object, _tenantRepositoryMock.Object);

            var catalogManager = new CatalogManager(_inventoryManager, _tenantRepositoryMock.Object,
                _reportGeneratorMock.Object);

            catalogManager.AddProduct(sku, description);
            var product = _inventoryManager.GetProductBySku(Constants.Configuration.Acquiree, sku);
            Assert.IsNull(product);
        }
    }
}