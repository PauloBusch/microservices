using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Discount.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{productName}")]
        public async Task<Coupon> GetAsync([FromRoute] string productName)
            => await _repository.GetAsync(productName);

        [HttpPost]
        public async Task<bool> PostAsync([FromBody] Coupon coupon)
            => await _repository.CreateAsync(coupon);

        [HttpPut]
        public async Task<bool> PutAsync([FromBody] Coupon coupon)
            => await _repository.UpdateAsync(coupon);

        [HttpDelete("{productName}")]
        public async Task<bool> DeleteAsync([FromRoute] string productName)
            => await _repository.DeleteAsync(productName);
    }
}
