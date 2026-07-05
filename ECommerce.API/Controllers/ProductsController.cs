using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{

    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Get All Prouducts
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts(CancellationToken ct)
        {
            var result = await _productService.GetAllProductsAsync(ct);

            return ToActionResult(result);
        }

        // Get Product By Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct( int id , CancellationToken ct)
        {
            var result = await _productService.GetProductByIdAsync(id, ct);
            return ToActionResult(result);
        }

        //Get All Brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct)
        {
            return ToActionResult(await _productService.GetAllBrandsAsync(ct));
        }


        //Get All Types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct)
        {
            return ToActionResult(await _productService.GetAllTypsAsync(ct));
        }

    }
}
