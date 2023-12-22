using FileHelpers;

namespace OneCatalog.Infrastructure.Records
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class ProductSourceRecord
    {
        public string Sku { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
    }
}