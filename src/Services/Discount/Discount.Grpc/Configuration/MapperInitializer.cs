using AutoMapper;

namespace Discount.Grpc.Configuration
{
    static class MapperInitializer
    {
        public static IMapper GetConfiguredMapper()
        {
            return GetMapperConfiguration().CreateMapper();
        }

        public static MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Discount.Grpc.Protos.CouponModel, Discount.Domain.Model.Coupon>();
                cfg.CreateMap<Discount.Domain.Model.Coupon, Discount.Grpc.Protos.CouponModel>();
            });
        }
    }
}
