using AutoMapper;
using ECommerce.Application.Common;
using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.OrdersDtos;
using ECommerce.Application.Specification;
using ECommerce.Domain.Contracts;
using ECommerce.Domain.Entities.Orders;
using ECommerce.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderService(IBasketRepository basketRepository ,
                            IUnitOfWork unitOfWork ,
                            IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<Result<OrderToReturnDto>> CreateOrderAsync(OrderDto orderDto, string email, CancellationToken ct = default)
        {
            var basket = await basketRepository.GetBasketAsync(orderDto.BasketId, ct);
            if (basket == null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Not Found", "Basket With This Id Not Found"));
            if (basket.Items.Count == 0)
                return Result<OrderToReturnDto>.Fail(Error.Validation("Validation", "Basket Has No Items"));

            var orderItems = new List<OrderItem>(basket.Items.Count);
            var productsIds = basket.Items.Select(X => X.Id).ToHashSet();
            var products = (await unitOfWork.GetRepository<Product, int>().
                GetAllAsync(new ProductWithIdSpecification(productsIds), ct)).ToDictionary(X => X.Id);


            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Result<OrderToReturnDto>.Fail(Error.NotFound("Not Found", "Product Not Found"));

                orderItems.Add(new OrderItem
                {
                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrdered()
                    {
                        PictureUrl = product.PictureUrl,
                        ProductId = product.Id,
                        ProductName = product.Name,
                    }

                });
            }

            var orderAddress = mapper.Map<OrderAddress>(orderDto.ShipToAddress);

            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod ,  int>()
                .GetByIdAsync(orderDto.DeliveryMethodId);

            if (deliveryMethod == null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Not Found", "Delivery Method Not Found"));

            var supTotal = orderItems.Sum(x => x.Price * x.Quantity);

            var order = new Order(email, orderAddress, orderItems, deliveryMethod, supTotal);

            unitOfWork.GetRepository<Order, Guid>().Add(order);
            var result = await unitOfWork.SaveChangesAsync(ct);
            if(result == 0)
            {
                return Result<OrderToReturnDto>.Fail(Error.Failure(description: "Cannot Add Order"));
            }
            else
            {
                await basketRepository.DeleteBasketAsync(orderDto.BasketId , ct);
                return mapper.Map<OrderToReturnDto>(order);
            }
        }
    }
}
