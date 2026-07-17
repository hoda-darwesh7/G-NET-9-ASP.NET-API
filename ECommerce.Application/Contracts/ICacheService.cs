using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface ICacheService
    {
        Task<string?> GetDataAsync(string cachekey , CancellationToken ct = default);
        Task SetDataAsync(string cachekey , object cacheValue , TimeSpan? TimeToLive = default , CancellationToken ct = default);
    }
}
