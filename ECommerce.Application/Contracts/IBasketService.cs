using ECommerce.Application.Common;
using ECommerce.Application.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IBasketService
    {
        Task<Result<BasketDto>> GetBasketAsync(string basketId , CancellationToken ct = default);
        Task<Result<BasketDto>> CreateOrUpdateBasketAsync(BasketDto basket , TimeSpan? TLV = default , CancellationToken ct = default);
        Task<Result<bool>> DeletBasketAsync(string basketId , CancellationToken ct = default);
    }
}
