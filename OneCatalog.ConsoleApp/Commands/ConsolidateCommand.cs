using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using OneCatalog.Application;
using OneCatalog.Application.Interfaces.Services;

namespace OneCatalog.ConsoleApp.Commands
{
    public class ConsolidateCommand : Command
    {
        private readonly ICatalogManager _catalogManager;

        public ConsolidateCommand(ICatalogManager catalogManager) : base("consolidate",
            "Merges acquirer and acquiree catalog")
        {
            _catalogManager = catalogManager;
        }

        public override IEnumerable<CommandOption> Options
        {
            get { yield return new CommandOption(Constants.Configuration.ResultFileName, "Full path of result file"); }
        }

        public override void Execute()
        {
            _catalogManager.Consolidate();
        }
    }
}