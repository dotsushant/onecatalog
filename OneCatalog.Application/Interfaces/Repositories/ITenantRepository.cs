namespace OneCatalog.Application.Interfaces.Repositories
{
    public interface ITenantRepository
    {
        Tenant Acquirer { get; }
        Tenant Acquiree { get; }

        Tenant GetById(string id);
    }
}