using AutoMapper;

namespace Coupon.Grpc.Configuration
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
                cfg.CreateMap<CouponModel, Discount.Domain.Model.Coupon>();
                cfg.CreateMap<Discount.Domain.Model.Coupon, CouponModel>();
            });
        }
    }
}
