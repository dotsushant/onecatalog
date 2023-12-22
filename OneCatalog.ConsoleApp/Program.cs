using System;
using Microsoft.Extensions.Configuration;
using OneCatalog.Application.Interfaces.Repositories;
using OneCatalog.Application.Interfaces.Services;
using OneCatalog.Application.Services;
using OneCatalog.ConsoleApp.Commands;
using OneCatalog.Infrastructure.Repositories;
using OneCatalog.Infrastructure.Services;
using SimpleInjector;

namespace OneCatalog.ConsoleApp
{
    internal class Program
    {
        private Container _container;

        private static void Main(string[] args)
        {
            try
            {
                new Program()
                    .BuildContainer()
                    .BuildCommands()
                    .Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Run()
        {
            _container.GetInstance<CommandProcessor>().Process();
        }

        private Program BuildContainer()
        {
            _container = new Container();

            // Configuration
            _container.RegisterSingleton<IConfiguration>(() =>
                new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true)
                    .AddCommandLine(Environment.GetCommandLineArgs()).Build());

            // Command processor
            _container.RegisterSingleton<CommandProcessor>();

            // Commands
            _container.Register<ConsolidateCommand>();
            _container.Register<AddProductCommand>();
            _container.Register<RemoveProductCommand>();
            _container.Register<AddProductSupplierCommand>();

            // Repositories
            _container.RegisterSingleton<ITenantRepository, TenantRepository>();
            _container.RegisterSingleton<IProductRepository, ProductRepository>();
            _container.RegisterSingleton<ISupplierRepository, SupplierRepository>();
            _container.RegisterSingleton<ISupplierProductBarcodeRepository, SupplierProductBarcodeRepository>();

            // Infrastructure services
            _container.RegisterSingleton<IReportGenerator, ReportGenerator>();

            // Domain services
            _container.RegisterSingleton<IInventoryManager, InventoryManager>();
            _container.RegisterSingleton<ICatalogManager, CatalogManager>();

            _container.Verify();
            return this;
        }

        private Program BuildCommands()
        {
            var commandProcessor = _container.GetInstance<CommandProcessor>();
            commandProcessor.Add(_container.GetInstance<ConsolidateCommand>());
            commandProcessor.Add(_container.GetInstance<AddProductCommand>());
            commandProcessor.Add(_container.GetInstance<RemoveProductCommand>());
            commandProcessor.Add(_container.GetInstance<AddProductSupplierCommand>());
            return this;
        }
    }
}