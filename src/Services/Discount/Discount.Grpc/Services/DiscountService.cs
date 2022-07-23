using AutoMapper;
using Discount.Domain.Model;
using Discount.Domain.Repository.Interfaces;
using Discount.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
        
            if (coupon == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
            }

            return _mapper.Map<CouponModel>(coupon);
        }

        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon;
            await _discountRepository.CreateDiscount(_mapper.Map<Coupon>(coupon));

            return coupon;
        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon;

            await _discountRepository.UpdateDiscount(_mapper.Map<Coupon>(coupon));

            return coupon;
        }

        public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return new DeleteDiscountResponse()
            {
                Success = await _discountRepository.DeleteDiscount(request.ProductName)
            };
        }
    }
}
