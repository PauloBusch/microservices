using Discount.API.Entities;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetAsync(string productName);
        Task<bool> CreateAsync(Coupon coupon);
        Task<bool> UpdateAsync(Coupon coupon);
        Task<bool> DeleteAsync(string productName);
    }
}
