using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task DeleteAsync(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetAsync(string userName)
        {
            var basketJson = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrWhiteSpace(basketJson)) return default;
            return JsonSerializer.Deserialize<ShoppingCart>(basketJson);
        }

        public async Task<ShoppingCart> UpdateAsync(ShoppingCart basket)
        {
            var basketJson = JsonSerializer.Serialize(basket);
            await _redisCache.SetStringAsync(basket.UserName, basketJson);
            return await GetAsync(basket.UserName);
        }
    }
}
