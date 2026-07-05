using AutoMapper;
using ECommerce.Application.DTOs.Products;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<ProductBrand ,  BrandDto>();
            CreateMap<ProductType ,  TypeDto>();
            CreateMap<Product ,  ProductDto>()
                .ForMember(dst => dst.ProductBrand , opt => opt.MapFrom(src => src.ProductBrand.Name))
                .ForMember(dst => dst.ProductType , opt => opt.MapFrom(src => src.ProductType.Name));
             
        }
    }
}
