using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using OneCatalog.Application.Interfaces.Services;

namespace OneCatalog.ConsoleApp.Commands
{
    public class RemoveProductCommand : Command
    {
        private const string SkuOption = "sku";

        private readonly IConfiguration _configuration;
        private readonly ICatalogManager _catalogManager;

        public RemoveProductCommand(IConfiguration configuration, ICatalogManager catalogManager) : base(
            "removeProduct", "Removes an existing product from acquirer catalog")
        {
            _configuration = configuration;
            _catalogManager = catalogManager;
        }

        public override IEnumerable<CommandOption> Options
        {
            get { yield return new CommandOption(SkuOption, "Product sku"); }
        }

        public override void Execute()
        {
            _catalogManager.RemoveProduct(_configuration[SkuOption]);
        }
    }
}