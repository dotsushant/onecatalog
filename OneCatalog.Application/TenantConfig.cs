using OneCatalog.Application.Exceptions;
using OneCatalog.Helpers;

namespace OneCatalog.Application
{
    public class TenantConfig
    {
        public TenantConfig(string productsDataSource, string suppliersDataSource,
            string supplierProductBarcodesDataSource)
        {
            Contract.Requires<TenantException>(!string.IsNullOrWhiteSpace(productsDataSource),
                Resource.TenantDataSourceIsInvalid);
            Contract.Requires<TenantException>(!string.IsNullOrWhiteSpace(suppliersDataSource),
                Resource.TenantDataSourceIsInvalid);
            Contract.Requires<TenantException>(!string.IsNullOrWhiteSpace(supplierProductBarcodesDataSource),
                Resource.TenantDataSourceIsInvalid);

            ProductsDataSource = productsDataSource;
            SuppliersDataSource = suppliersDataSource;
            SupplierProductBarcodesDataSource = supplierProductBarcodesDataSource;
        }

        public string ProductsDataSource { get; }

        public string SuppliersDataSource { get; }

        public string SupplierProductBarcodesDataSource { get; }
    }
}