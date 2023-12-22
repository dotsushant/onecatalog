using NUnit.Framework;
using OneCatalog.Application;
using OneCatalog.Application.Exceptions;

namespace OneCatalog.Tests.Application
{
    [TestFixture]
    class TenantTests
    {
        [TestCase(null, null, null)]
        [TestCase("", "", null)]
        [TestCase(" ", " ", null)]
        public void ShouldThrowExceptionWhenTenantCreationParametersAreInvalid(string id, string name,
            TenantConfig config)
        {
            Assert.Throws<TenantException>(() => new Tenant(id, name, config));
        }

        [TestCase(null, null, null)]
        [TestCase("", "", "")]
        [TestCase(" ", " ", " ")]
        public void ShouldThrowExceptionWhenTenantConfigCreationParametersAreInvalid(string productsDataSource,
            string suppliersDataSource,
            string supplierProductBarcodesDataSource)
        {
            Assert.Throws<TenantException>(() =>
                new TenantConfig(productsDataSource, suppliersDataSource, supplierProductBarcodesDataSource));
        }

        [TestCase]
        public void ShouldInitializeWhenTenantCreationParametersAreValid()
        {
            var id = "a";
            var name = "b";
            var tenantConfig = new TenantConfig("p", "s", "ps");
            var tenant = new Tenant(id, name, tenantConfig);
            Assert.AreEqual(tenant.Id, id);
            Assert.AreEqual(tenant.Name, name);
            Assert.AreEqual(tenant.Config.ProductsDataSource, tenantConfig.ProductsDataSource);
            Assert.AreEqual(tenant.Config.SuppliersDataSource, tenantConfig.SuppliersDataSource);
            Assert.AreEqual(tenant.Config.SupplierProductBarcodesDataSource,
                tenantConfig.SupplierProductBarcodesDataSource);
        }

        [TestCase]
        public void ShouldInitializeWhenTenantConfigCreationParametersAreValid()
        {
            string productsDataSource = "a", suppliersDataSource = "b", supplierProductBarcodesDataSource = "c";
            var tenantConfig =
                new TenantConfig(productsDataSource, suppliersDataSource, supplierProductBarcodesDataSource);

            Assert.AreEqual(tenantConfig.ProductsDataSource, productsDataSource);
            Assert.AreEqual(tenantConfig.SuppliersDataSource, suppliersDataSource);
            Assert.AreEqual(tenantConfig.SupplierProductBarcodesDataSource, supplierProductBarcodesDataSource);
        }
    }
}