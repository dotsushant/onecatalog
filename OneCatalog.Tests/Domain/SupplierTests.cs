using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OneCatalog.Domain.Exceptions;
using OneCatalog.Domain.Models;

namespace OneCatalog.Tests.Domain
{
    [TestFixture]
    class SupplierTests
    {
        [TestCase(1, null)]
        [TestCase(1, "")]
        [TestCase(10, " ")]
        public void ShouldThrowExceptionWhenSupplierCreationParametersAreInvalid(int id, string name)
        {
            Assert.Throws<SupplierException>(() => new Supplier(id, name));
        }

        [TestCase]
        public void ShouldCreateSupplierWhenValidCreationParametersAreProvided()
        {
            var id = 10;
            var name = "some name";
            var supplier = new Supplier(id, name);
            Assert.AreEqual(supplier.Id, id);
            Assert.AreEqual(supplier.Name, name);
        }

        [TestCase]
        public void ShouldReturnTrueIfBarcodeExists()
        {
            var barcode = "b4381274928349";
            var supplier = new Supplier(1, "Trunyx");
            supplier.AddBarcode(barcode);
            Assert.IsTrue(supplier.Barcodes.Contains(barcode));
        }

        [TestCase]
        public void ShouldReturnFalseIfBarcodeDoesNotExist()
        {
            var barcode = "b4381274928349";
            var supplier = new Supplier(1, "Trunyx");
            Assert.IsFalse(supplier.Barcodes.Contains(barcode));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void ShouldThrowExceptionWhenInvalidBarcodeIsAdded(string barcode)
        {
            var supplier = new Supplier(1, "Trunyx");
            Assert.Throws<SupplierException>(() => supplier.AddBarcode(barcode));
        }

        [TestCase((object) new string[0])]
        [TestCase((object) new[] {""})]
        [TestCase((object) new[] {(string) null})]
        [TestCase((object) new[] {"b4381274928349", ""})]
        [TestCase((object) new[] {"b4381274928349", " "})]
        [TestCase((object) new[] {"b4381274928349", (string) null})]
        public void ShouldThrowExceptionWhenInvalidBarcodesAreAdded(string[] barcodes)
        {
            var supplier = new Supplier(1, "Trunyx");
            Assert.Throws<SupplierException>(() => supplier.AddBarcodes(barcodes));
        }
    }
}