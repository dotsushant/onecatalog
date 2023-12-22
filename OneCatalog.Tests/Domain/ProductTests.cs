using NUnit.Framework;
using OneCatalog.Domain.Exceptions;
using OneCatalog.Domain.Models;

namespace OneCatalog.Tests.Domain
{
    [TestFixture]
    class TenantTests
    {
        [TestCase(null, null)]
        [TestCase(null, "Cheese cake")]
        [TestCase("999-vyk-317", null)]
        [TestCase("", "")]
        [TestCase("", "Cheese cake")]
        [TestCase("999-vyk-317", "")]
        [TestCase(" ", " ")]
        [TestCase(" ", "Cheese cake")]
        [TestCase("999-vyk-317", " ")]
        public void ShouldThrowExceptionWhenProductCreationParametersAreInvalid(string sku, string description)
        {
            Assert.Throws<ProductException>(() => new Product(sku, description));
        }

        [TestCase]
        public void ShouldCreateProductWhenValidCreationParametersAreProvided()
        {
            string sku = "999-vyk-317", description = "Cheese cake";
            var product = new Product(sku, description);
            Assert.AreEqual(product.Sku, sku);
            Assert.AreEqual(product.Description, description);
        }

        [TestCase]
        public void ShouldReturnTrueIfBarcodeExists()
        {
            var barcode = "b4381274928349";
            var product = new Product("999-vyk-317", "Cheese cake");
            var supplier = new Supplier(1, "Trunyx");
            supplier.AddBarcode(barcode);
            product.AddSupplier(supplier);
            Assert.IsTrue(product.HasBarcode(barcode));
        }

        [TestCase]
        public void ShouldReturnFalseIfBarcodeDoesNotExist()
        {
            var barcode = "b4381274928349";
            var product = new Product("999-vyk-317", "Cheese cake");
            var supplier = new Supplier(1, "Trunyx");
            product.AddSupplier(supplier);
            Assert.IsFalse(product.HasBarcode(barcode));
        }

        [TestCase]
        public void ShouldThrowExceptionWhenInvalidSupplierIsAdded()
        {
            Assert.Throws<ProductException>(() =>
            {
                var product = new Product("999-vyk-317", "Cheese cake");
                product.AddSupplier(null);
            });
        }
    }
}