using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Specification;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Products;

namespace ECommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand , int>().GetAllAsync(ct);

            var data = _mapper.Map<IReadOnlyList<BrandDto>>(brands);

            return Result<IReadOnlyList<BrandDto>>.Ok(data);
        }

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(int? brandId, int? typeId, CancellationToken ct = default)
        {
            var Spec = new ProductWithBrandAndTypeSpec( brandId , typeId );

            var products = await _unitOfWork.GetRepository<Product , int>().GetAllAsync(Spec, ct);

            return Result<IReadOnlyList<ProductDto>>.Ok(_mapper.Map<IReadOnlyList<ProductDto>>(products));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypsAsync(CancellationToken ct = default)
        {
            var types = await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct);

            var data = _mapper.Map<IReadOnlyList<TypeDto>>(types);

            return Result<IReadOnlyList<TypeDto>>.Ok(data);
        }

        public async Task<Result<ProductDto>> GetProductByIdAsync(int id, CancellationToken ct = default)
        {
            var Spec = new ProductWithBrandAndTypeSpec(id);

            var product = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(Spec, ct);

            if (product == null) 
                 return Error.NotFound("Product.NotFound" , $"Product With Id {id} is Not Found");

            return _mapper.Map<ProductDto>(product);

        }
    }
}
