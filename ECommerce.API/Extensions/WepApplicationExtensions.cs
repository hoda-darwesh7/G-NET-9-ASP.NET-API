using ECommerce.Domain.Contracts;
using Microsoft.AspNetCore.Builder;

namespace ECommerce.API.Extensions
{
    public static class WepApplicationExtensions
    {
        public static async Task<WebApplication> SeedAndMigrateDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var seeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");
            var Identityseeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Identity");

            await seeder.SeedDataAsync();
            await Identityseeder.SeedDataAsync();
            return app;
        }
    }
}
