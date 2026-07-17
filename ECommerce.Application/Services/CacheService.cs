using ECommerce.Application.Contracts;
using ECommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICacheRepository _cacheRepo;

        public CacheService(ICacheRepository cacheRepo)
        {
            _cacheRepo = cacheRepo;
        }

        public async Task<string?> GetDataAsync(string cachekey, CancellationToken ct = default) 
            => await _cacheRepo.GetAsync(cachekey, ct);


        public async Task SetDataAsync(string cachekey, object cacheValue, TimeSpan? TimeToLive = null, CancellationToken ct = default)
        {
            var jsonValue = JsonSerializer.Serialize(cacheValue , new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            await _cacheRepo.SetAsync(cachekey, jsonValue, TimeToLive, ct);
        }
    }
}
