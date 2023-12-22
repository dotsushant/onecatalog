using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using OneCatalog.Application.Interfaces.Services;

namespace OneCatalog.ConsoleApp.Commands
{
    public class AddProductSupplierCommand : Command
    {
        private const char BarcodesDelimiter = ';';

        private const string SkuOption = "sku";
        private const string SupplierOption = "supplier";
        private const string BarcodesOption = "barcodes";

        private readonly IConfiguration _configuration;
        private readonly ICatalogManager _catalogManager;

        public AddProductSupplierCommand(IConfiguration configuration, ICatalogManager catalogManager) : base(
            "addProductSupplier", "Adds a new supplier with barcodes for an existing product to acquiree catalog")
        {
            _configuration = configuration;
            _catalogManager = catalogManager;
        }

        public override IEnumerable<CommandOption> Options
        {
            get
            {
                yield return new CommandOption(SkuOption, "Product sku");
                yield return new CommandOption(SupplierOption, "Supplier name");
                yield return new CommandOption(BarcodesOption, $"List of barcodes delimited by {BarcodesDelimiter}");
            }
        }

        public override void Execute()
        {
            _catalogManager.AddSupplier(_configuration[SkuOption], _configuration[SupplierOption],
                _configuration[BarcodesOption].Split(BarcodesDelimiter));
        }
    }
}