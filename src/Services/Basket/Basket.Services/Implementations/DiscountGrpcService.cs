using AutoMapper;
using Basket.Services.Interfaces;
using Basket.Services.Models;
using Basket.Services.Models.ServiceDTOs;
using Discount.Grpc.Protos;
using Grpc.Net.Client;

namespace Basket.Services.Implementations
{
    public class DiscountGrpcService : IDiscountService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _grpcDiscountClient;
        private readonly IMapper _mapper;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient grpcDiscountClient, IMapper mapper)
        {
            _grpcDiscountClient = grpcDiscountClient;
            _mapper = mapper;
        }

        public async Task<ServiceValueResult<CouponDTO>> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest() { ProductName = productName };
            CouponModel result = await _grpcDiscountClient.GetDiscountAsync(discountRequest);           

            return ServiceValueResult<CouponDTO>.Success(_mapper.Map<CouponDTO>(result));
        }
    }
}
