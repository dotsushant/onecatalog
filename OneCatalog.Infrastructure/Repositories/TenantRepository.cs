using System;
using Microsoft.Extensions.Configuration;
using OneCatalog.Application;
using OneCatalog.Application.Interfaces.Repositories;

namespace OneCatalog.Infrastructure.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        private readonly IConfiguration _configuration;

        public TenantRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Tenant Acquirer => CreateTenant(Constants.Configuration.Acquirer);
        public Tenant Acquiree => CreateTenant(Constants.Configuration.Acquiree);

        public Tenant GetById(string id)
        {
            if (string.Equals(Acquirer.Id, id, StringComparison.OrdinalIgnoreCase))
                return Acquirer;
            if (string.Equals(Acquiree.Id, id, StringComparison.OrdinalIgnoreCase))
                return Acquiree;

            return null;
        }

        private Tenant CreateTenant(string id)
        {
            return new Tenant(id, _configuration[$"{id}:{Constants.Configuration.DisplayName}"],
                new TenantConfig(_configuration[$"{id}:{Constants.Configuration.ProductsDataSource}"],
                    _configuration[$"{id}:{Constants.Configuration.SuppliersDataSource}"],
                    _configuration[$"{id}:{Constants.Configuration.SupplierProductBarcodesDataSource}"]));
        }
    }
}