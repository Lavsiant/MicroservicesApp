using AutoMapper;
using Basket.API.ViewModels;
using Basket.Domain.Model;
using Basket.Services.Models.ServiceDTOs;
using Discount.Grpc.Protos;

namespace Basket.API.Configuration
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
                ConfigureViewModelDTO(cfg);
                ConfigureDTODomain(cfg);
            });
        }

        private static void ConfigureViewModelDTO(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ShoppingCartItemViewModel, ShoppingCartItemDTO>().ReverseMap();
            cfg.CreateMap<ShoppingCartViewModel, ShoppingCartDTO>().ReverseMap();
        }

        private static void ConfigureDTODomain(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<ShoppingCartItemDTO, ShoppingCartItem>().ReverseMap();
            cfg.CreateMap<ShoppingCartDTO, ShoppingCart>().ReverseMap(); 
            cfg.CreateMap<CouponModel, CouponDTO>().ReverseMap();
        }

    }
}
