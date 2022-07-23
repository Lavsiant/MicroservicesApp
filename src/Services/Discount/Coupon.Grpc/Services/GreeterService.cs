using AutoMapper;
//using Coupon.Grpc;
//using Discount.Domain.Repository.Interfaces;
//using Grpc.Core;

//namespace Coupon.Grpc.Services
//{
//    public class GreeterService : Greeter.GreeterBase
//    {
//        private readonly ILogger<GreeterService> _logger;
//        private readonly IDiscountRepository _discountRepository;
//        private readonly IMapper _mapper;

//        public GreeterService(ILogger<GreeterService> logger, IMapper mapper, IDiscountRepository discountRepository)
//        {
//            _logger = logger;
//            _mapper = mapper;
//            _discountRepository = discountRepository;
//        }

//        //public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
//        //{
//        //    return Task.FromResult(new HelloReply
//        //    {
//        //        Message = "Hello " + request.Name
//        //    });
//        //}


//        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
//        {
//            var coupon = await _discountRepository.GetDiscount(request.ProductName);

//            if (coupon == null)
//            {
//                throw new RpcException(new Status(StatusCode.NotFound, "Discount not found"));
//            }

//            try
//            {
//                var x = _mapper.Map<CouponModel>(coupon); ;
//            } catch (Exception ex)
//            {
//                Console.WriteLine();
//            }

//            return _mapper.Map<CouponModel>(coupon);
//        }
//    }
//}