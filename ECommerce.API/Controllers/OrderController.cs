using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.OrdersDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto , CancellationToken ct)
        {
            return ToActionResult(await orderService.CreateOrderAsync(orderDto, GetEmailFromToken() , ct));
        }
    }
}
