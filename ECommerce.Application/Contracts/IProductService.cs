using ECommerce.Application.Common;
using ECommerce.Application.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Contracts
{
    public interface IProductService
    {
        Task<Result< IReadOnlyList<ProductDto>>> GetAllProductsAsync( int? brandId , int? typeId , CancellationToken ct  = default);
        Task<Result< IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct  = default);
        Task<Result< IReadOnlyList<TypeDto>>> GetAllTypsAsync(CancellationToken ct  = default);

        Task<Result<ProductDto>> GetProductByIdAsync(int id , CancellationToken ct = default);

    }
}
