using System.Linq;
using Microsoft.Extensions.Configuration;
using OneCatalog.Application;
using OneCatalog.Application.Interfaces.Repositories;
using OneCatalog.Application.Interfaces.Services;
using OneCatalog.Helpers;
using OneCatalog.Infrastructure.Extensions;

namespace OneCatalog.Infrastructure.Services
{
    public class ReportGenerator : IReportGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly ITenantRepository _tenantRepository;

        public ReportGenerator(ITenantRepository tenantRepository, IConfiguration configuration)
        {
            _tenantRepository = tenantRepository;
            _configuration = configuration;
        }

        public void GenerateReport(ReportData reportData)
        {
            var acquirerProductSourceRecords = reportData.AcquirerProducts
                .ToProductSourceRecords(_tenantRepository.Acquirer.Name);

            var acquireeProductSourceRecords =
                reportData.AcquireeProducts.ToProductSourceRecords(_tenantRepository.Acquiree.Name);

            CsvHelper.WriteRecords(_configuration[Constants.Configuration.ResultFileName],
                acquirerProductSourceRecords.Union(acquireeProductSourceRecords));
        }
    }
}