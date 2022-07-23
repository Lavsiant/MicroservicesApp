using AutoMapper;
using Ordering.API.ViewModels;
using Ordering.Domain.Aggregates.OrderModule;
using Ordering.Domain.DTO;
using Ordering.Domain.ReadModel.Model;

namespace Ordering.API.Configuration
{
    public class MapperInitializer
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
                ConfigureEventToDomain(cfg);
            });
        }

        private static void ConfigureViewModelDTO(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<AddOrderViewModel, OrderDTO>().ReverseMap(); 
            cfg.CreateMap<UpdateOrderViewModel, OrderDTO>().ReverseMap();
            cfg.CreateMap<OrderViewModel, OrderDTO>().ReverseMap();
            cfg.CreateMap<AddressViewModel, AddressDTO>().ReverseMap();
            cfg.CreateMap<CardViewModel, CardDTO>().ReverseMap();
        }

        private static void ConfigureDTODomain(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrderDTO, Order>().ReverseMap();
            cfg.CreateMap<AddressDTO, Address>().ReverseMap();
            cfg.CreateMap<CardDTO, Card>().ReverseMap();
        }

        private static void ConfigureEventToDomain(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<OrderCreatedEvent, Order>();
        }
    }
}
