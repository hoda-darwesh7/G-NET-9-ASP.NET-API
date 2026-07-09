using ECommerce.Application.Common;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Specification
{
    public class ProductWithBrandAndTypeSpec : BaseSpecification<Product , int>
    {
        public ProductWithBrandAndTypeSpec(ProductQueryParams queryParams) : base
            (p => (!queryParams.BrandId.HasValue || p.BrandId == queryParams.BrandId.Value) 
            && (!queryParams.TypeId.HasValue || p.TypeId == queryParams.TypeId.Value)
            && (string.IsNullOrWhiteSpace(queryParams.Search) || p.Name.ToLower().Contains(queryParams.Search.ToLower())))
        {
            AddInclude(b => b.ProductBrand);
            AddInclude(b => b.ProductType);

            switch(queryParams.Sort)
            {
                case ProductSortOptions.NameAsc:
                    AddOrderBy(b => b.Name);
                    break;
                case ProductSortOptions.NameDesc:
                    AddOrderByDesc(b => b.Name); 
                    break;
                case ProductSortOptions.PriceAsc: 
                    AddOrderBy(b => b.Price);
                    break;
                case ProductSortOptions.PriceDesc:
                    AddOrderByDesc(b => b.Price);
                    break;
                default:
                    AddOrderBy (b => b.Id);
                    break;
            }

            ApplyPagination(queryParams.PageSize, queryParams.PageIndex);
        }

        public ProductWithBrandAndTypeSpec(int id) : base (P => P.Id == id)
        {
            AddInclude(b => b.ProductBrand);
            AddInclude(b => b.ProductType);
        }
    }
}
