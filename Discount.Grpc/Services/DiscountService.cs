using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;
using System.Threading.Tasks;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _repository;

        public DiscountService(IMapper mapper, IDiscountRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public override async Task<CouponModel> Get(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetAsync(request.ProductName);
            if (coupon == null) throw new RpcException(
                new Status(StatusCode.NotFound, $"Discount with ProductName: {request.ProductName} not found.")
            );

            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> Create(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _repository.CreateAsync(coupon);
            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<CouponModel> Update(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _repository.UpdateAsync(coupon);
            return _mapper.Map<CouponModel>(coupon);
        }

        public override async Task<DeleteDiscountResponse> Delete(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteAsync(request.ProductName);
            return new DeleteDiscountResponse { Success = deleted };
        }
    }
}
