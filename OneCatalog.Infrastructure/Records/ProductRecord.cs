using FileHelpers;

namespace OneCatalog.Infrastructure.Records
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class ProductRecord
    {
        public string Sku { get; set; }
        public string Description { get; set; }
    }
}