using FileHelpers;

namespace OneCatalog.Infrastructure.Records
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class SupplierProductBarcodeRecord
    {
        public int SupplierId { get; set; }
        public string ProductSku { get; set; }
        public string ProductBarcode { get; set; }
    }
}