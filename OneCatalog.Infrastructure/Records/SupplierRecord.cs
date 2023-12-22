using FileHelpers;

namespace OneCatalog.Infrastructure.Records
{
    [IgnoreFirst]
    [DelimitedRecord(",")]
    public class SupplierRecord
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}