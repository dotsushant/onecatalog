using System.Collections.Generic;
using OneCatalog.Application.Exceptions;
using OneCatalog.Domain.Models;
using OneCatalog.Helpers;

namespace OneCatalog.Application
{
    public class ReportData
    {
        public ReportData(IEnumerable<Product> acquirerProducts, IEnumerable<Product> acquireeProducts)
        {
            Contract.Requires<ReportException>(acquirerProducts != null && acquireeProducts != null,
                Resource.ReportDataIsInvalid);

            AcquirerProducts = acquirerProducts;
            AcquireeProducts = acquireeProducts;
        }

        public IEnumerable<Product> AcquirerProducts { get; }
        public IEnumerable<Product> AcquireeProducts { get; }
    }
}