using AutoMapper;
using Discount.Domain.Repository.Interfaces;
using Grpc.Core;

namespace Coupon.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
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

        //public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        //{
        //    var coupon = request.Coupon;
        //    await _discountRepository.CreateDiscount(_mapper.Map<Discount.Domain.Model.Coupon>(coupon));

        //    return coupon;
        //}

        //public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        //{
        //    var coupon = request.Coupon;

        //    await _discountRepository.UpdateDiscount(_mapper.Map<Discount.Domain.Model.Coupon>(coupon));

        //    return coupon;
        //}

        //public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        //{
        //    return new DeleteDiscountResponse()
        //    {
        //        Success = await _discountRepository.DeleteDiscount(request.ProductName)
        //    };
        //}
    }
}

