using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using OneCatalog.Application.Interfaces.Services;

namespace OneCatalog.ConsoleApp.Commands
{
    public class AddProductCommand : Command
    {
        private const string SkuOption = "sku";
        private const string DescriptionOption = "description";

        private readonly IConfiguration _configuration;
        private readonly ICatalogManager _catalogManager;

        public AddProductCommand(IConfiguration configuration, ICatalogManager catalogManager) : base(
            "addProduct", "Adds a new product to acquirer catalog")
        {
            _configuration = configuration;
            _catalogManager = catalogManager;
        }

        public override IEnumerable<CommandOption> Options
        {
            get
            {
                yield return new CommandOption(SkuOption, "Product sku");
                yield return new CommandOption(DescriptionOption, "Product description");
            }
        }

        public override void Execute()
        {
            _catalogManager.AddProduct(_configuration[SkuOption], _configuration[DescriptionOption]);
        }
    }
}