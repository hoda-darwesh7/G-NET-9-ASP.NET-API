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
        public ProductWithBrandAndTypeSpec(int? BrandId, int? TypeId) : base
            (p => (!BrandId.HasValue || p.BrandId == BrandId.Value) && (!TypeId.HasValue || p.TypeId == TypeId.Value))
        {
            AddInclude(b => b.ProductBrand);
            AddInclude(b => b.ProductType);
        }

        public ProductWithBrandAndTypeSpec(int id) : base (P => P.Id == id)
        {
            AddInclude(b => b.ProductBrand);
            AddInclude(b => b.ProductType);
        }
    }
}
