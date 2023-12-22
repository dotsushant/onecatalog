using OneCatalog.Application.Exceptions;
using OneCatalog.Helpers;

namespace OneCatalog.Application
{
    public class Tenant
    {
        public Tenant(string id, string name, TenantConfig config)
        {
            Contract.Requires<TenantException>(!string.IsNullOrWhiteSpace(id), Resource.TenantIdIsInvalid);
            Contract.Requires<TenantException>(!string.IsNullOrWhiteSpace(name), Resource.TenantNameIsInvalid);
            Contract.Requires<TenantException>(config != null, Resource.TenantConfigIsInvalid);

            Id = id;
            Name = name;
            Config = config;
        }

        public string Id { get; }

        public string Name { get; }

        public TenantConfig Config { get; }
    }
}