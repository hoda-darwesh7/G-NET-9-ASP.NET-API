using ECommerce.Application.Contracts;
using ECommerce.Application.DTOs.BasketDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    public class BasketController : ApiBaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string id , CancellationToken ct)
        {
            var result = await _basketService.GetBasketAsync(id, ct);
            return ToActionResult(result);
        }


        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket , CancellationToken ct)
        {
            var result = await _basketService.CreateOrUpdateBasketAsync(basket , ct:ct); 
            return ToActionResult(result);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id , CancellationToken ct)
        {
            var result = await _basketService.DeletBasketAsync(id, ct);
            return ToActionResult(result);
        }
    }
}
