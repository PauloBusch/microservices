using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(
            IBasketRepository repository,
            DiscountGrpcService discountGrpcService
        )
        {
            _repository = repository;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{userName}")]
        public async Task<ShoppingCart> GetAsync([FromRoute] string userName)
        {
            var basket = await _repository.GetAsync(userName);
            return basket ?? new ShoppingCart(userName);
        }

        [HttpPost]
        public async Task<ShoppingCart> PostAsync([FromBody] ShoppingCart basket)
        {
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscountAsync(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return await _repository.UpdateAsync(basket);
        }

        [HttpDelete("{userName}")]
        public async Task DeleteAsync([FromRoute] string userName)
            => await _repository.DeleteAsync(userName);
    }
}
