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
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;

        private readonly Dictionary<string, Dictionary<string, Product>> _tenantProductMap =
            new Dictionary<string, Dictionary<string, Product>>(StringComparer.OrdinalIgnoreCase);

        private readonly ITenantRepository _tenantRepository;

        public ProductRepository(IConfiguration configuration, ITenantRepository tenantRepository)
        {
            _configuration = configuration;
            _tenantRepository = tenantRepository;

            _tenantProductMap.Add(_tenantRepository.Acquirer.Id,
                new Dictionary<string, Product>(StringComparer.OrdinalIgnoreCase));
            _tenantProductMap.Add(_tenantRepository.Acquiree.Id,
                new Dictionary<string, Product>(StringComparer.OrdinalIgnoreCase));
        }

        public Product Add(string tenantId, string sku, string description)
        {
            if (!_tenantProductMap.ContainsKey(tenantId)) return null;

            _tenantProductMap.TryGetValue(tenantId, out var productMap);

            if (!productMap.ContainsKey(sku)) productMap[sku] = new Product(sku, description);

            return productMap[sku];
        }

        public void Remove(string tenantId, string sku)
        {
            if (!_tenantProductMap.ContainsKey(tenantId)) return;

            _tenantProductMap.TryGetValue(tenantId, out var productMap);
            if (productMap.ContainsKey(sku)) productMap.Remove(sku);
        }

        public Product GetBySku(string tenantId, string sku)
        {
            if (!_tenantProductMap.ContainsKey(tenantId)) return null;

            _tenantProductMap.TryGetValue(tenantId, out var productMap);
            productMap.TryGetValue(sku, out var product);
            return product;
        }

        public Product GetByBarcode(string tenantId, string barcode)
        {
            if (!_tenantProductMap.ContainsKey(tenantId)) return null;

            _tenantProductMap.TryGetValue(tenantId, out var productMap);
            return productMap.Values.FirstOrDefault(product => product.HasBarcode(barcode));
        }

        public IEnumerable<Product> GetAll(string tenantId)
        {
            if (!_tenantProductMap.ContainsKey(tenantId)) return Enumerable.Empty<Product>();

            _tenantProductMap.TryGetValue(tenantId, out var productMap);
            return productMap.Values;
        }

        public void Load()
        {
            _tenantProductMap.Keys.ForEach(LoadInitialData);
        }

        public void Save(string tenantId)
        {
            if (!_tenantProductMap.ContainsKey(tenantId)) return;

            _tenantProductMap.TryGetValue(tenantId, out var productMap);

            var datasetLocation = _configuration[Constants.Configuration.DatasetLocation];
            var productsDataSource = _tenantRepository.GetById(tenantId)?.Config.ProductsDataSource;
            CsvHelper.WriteRecords(Path.Combine(datasetLocation, productsDataSource),
                productMap.Values.Select(p => p.ToProductRecord()));
        }

        private void LoadInitialData(string tenantId)
        {
            var productsDataSource = Path.Combine(_configuration[Constants.Configuration.DatasetLocation],
                _configuration[$"{tenantId}:{Constants.Configuration.ProductsDataSource}"]);
            var productRecords = CsvHelper.ReadRecords<ProductRecord>(productsDataSource);
            productRecords.ForEach(productRecord =>
                Add(tenantId, productRecord.Sku, productRecord.Description));
        }
    }
}