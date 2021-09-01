using Discount.Grpc.Entities;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetAsync(string productName);
        Task<bool> CreateAsync(Coupon coupon);
        Task<bool> UpdateAsync(Coupon coupon);
        Task<bool> DeleteAsync(string productName);
    }
}
