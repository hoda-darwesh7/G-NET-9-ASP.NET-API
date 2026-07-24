using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;
using ECommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.DataSeeding
{
    public class CatalogDataSeeder : IDataSeeder
    {
        private readonly StoreDbContext _dbContext;
        private readonly ILogger _logger;

        public CatalogDataSeeder(StoreDbContext dbContext , ILogger<CatalogDataSeeder> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync(ct);
                if (pendingMigrations.Any())
                {
                    await _dbContext.Database.MigrateAsync(ct);
                }

                var seedRoot = Path.Combine(AppContext.BaseDirectory, "DataSeed");

                await SeedIfEmptyAsync<ProductBrand, int>(seedRoot, "brands.json", ct);
                await SeedIfEmptyAsync<ProductType, int>(seedRoot, "types.json", ct);
                await SeedIfEmptyAsync<Product, int>(seedRoot, "products.json", ct);
                await SeedIfEmptyAsync<DeliveryMethod, int>(seedRoot, "delivery.json", ct);

                int result = await _dbContext.SaveChangesAsync(ct);

                if (result > 0)
                    _logger.LogInformation($"{result} Rows Added");
                else
                    _logger.LogInformation($"Data Already Seeded");
               

            }
            catch (Exception ex)
            {

            }
        }

        private async Task SeedIfEmptyAsync<T , TKey>(string rootPath , string fileName ,CancellationToken ct = default) where T : BaseEntity<TKey>
        {
            if ( await _dbContext.Set<T>().AnyAsync())
            {
                _logger.LogInformation("Data Already Seeded");
                return;
            }

            var filepath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filepath))
            {
                _logger.LogWarning($" File {fileName} Is Not exists.");
                return;
            }

            using var fileStream = File.OpenRead(filepath);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream , options , ct);
            if (items?.Any() ?? false)
                _dbContext.Set<T>().AddRange(items);

        }
    }
}
