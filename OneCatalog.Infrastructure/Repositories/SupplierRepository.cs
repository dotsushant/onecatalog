using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using OneCatalog.Application;
using OneCatalog.Application.Interfaces.Repositories;
using OneCatalog.Domain.Models;
using OneCatalog.Helpers;
using OneCatalog.Infrastructure.Extensions;
using OneCatalog.Infrastructure.Records;

namespace OneCatalog.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ITenantRepository _tenantRepository;

        private readonly Dictionary<string, Dictionary<int, Supplier>> _tenantSupplierMap =
            new Dictionary<string, Dictionary<int, Supplier>>(StringComparer.OrdinalIgnoreCase);

        public SupplierRepository(IConfiguration configuration, ITenantRepository tenantRepository)
        {
            _configuration = configuration;
            _tenantRepository = tenantRepository;
            _tenantSupplierMap.Add(_tenantRepository.Acquirer.Id, new Dictionary<int, Supplier>());
            _tenantSupplierMap.Add(_tenantRepository.Acquiree.Id, new Dictionary<int, Supplier>());
        }

        public Supplier Add(string tenantId, int id, string name)
        {
            if (!_tenantSupplierMap.ContainsKey(tenantId)) return null;

            _tenantSupplierMap.TryGetValue(tenantId, out var supplierMap);

            if (!supplierMap.ContainsKey(id)) supplierMap[id] = new Supplier(id, name);
            return supplierMap[id];
        }

        public Supplier GetById(string tenantId, int id)
        {
            if (!_tenantSupplierMap.ContainsKey(tenantId)) return null;

            _tenantSupplierMap.TryGetValue(tenantId, out var supplierMap);
            supplierMap.TryGetValue(id, out var supplier);
            return supplier;
        }

        public IEnumerable<Supplier> GetAll(string tenantId)
        {
            if (!_tenantSupplierMap.ContainsKey(tenantId)) return null;

            _tenantSupplierMap.TryGetValue(tenantId, out var supplierMap);
            return supplierMap.Values;
        }

        public IEnumerable<Supplier> GetAllByBarcode(string tenantId, string barcode)
        {
            if (!_tenantSupplierMap.ContainsKey(tenantId)) return null;

            _tenantSupplierMap.TryGetValue(tenantId, out var supplierMap);
            return supplierMap.Values.Where(supplier => supplier.Barcodes.Contains(barcode));
        }

        public void Load()
        {
            _tenantSupplierMap.Keys.ForEach(LoadInitialData);
        }

        public void Save(string tenantId)
        {
            if (!_tenantSupplierMap.ContainsKey(tenantId)) return;

            _tenantSupplierMap.TryGetValue(tenantId, out var supplierMap);
            var datasetLocation = _configuration[Constants.Configuration.DatasetLocation];
            var suppliersDataSource = _tenantRepository.GetById(tenantId)?.Config.SuppliersDataSource;
            CsvHelper.WriteRecords(Path.Combine(datasetLocation, suppliersDataSource),
                supplierMap.Values.Select(p => p.ToSupplierRecord()));
        }

        private void LoadInitialData(string tenantId)
        {
            var suppliersDataSource = Path.Combine(_configuration[Constants.Configuration.DatasetLocation],
                _configuration[$"{tenantId}:{Constants.Configuration.SuppliersDataSource}"]);
            var supplierRecords = CsvHelper.ReadRecords<SupplierRecord>(suppliersDataSource);
            supplierRecords.ForEach(supplierRecord =>
                Add(tenantId, supplierRecord.Id, supplierRecord.Name));
        }
    }
}