using AutoMapper;
using ECommerce.Application.DTOs.IdentityDtos;
using ECommerce.Application.DTOs.OrdersDtos;
using ECommerce.Domain.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto , OrderAddress>().ReverseMap();

            CreateMap<Order , OrderToReturnDto>()
                .ForMember(X => X.DeliveryMethod , opt => opt.MapFrom(X => X.DeliveryMethod.ShortName))
                .ForMember(X => X.DeliveryMethodCost , opt => opt.MapFrom(X => X.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(X => X.ProducrId, opt => opt.MapFrom(X => X.Product.ProductId))
                .ForMember(X => X.ProductName, opt => opt.MapFrom(X => X.Product.ProductName))
                .ForMember(X => X.PictureUrl, opt => opt.MapFrom<OrderPictureUrlResolver>());
        }
    }
}
