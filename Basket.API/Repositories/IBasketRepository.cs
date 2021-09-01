using Basket.API.Entities;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetAsync(string userName);
        Task<ShoppingCart> UpdateAsync(ShoppingCart basket);
        Task DeleteAsync(string userName);
    }
}
