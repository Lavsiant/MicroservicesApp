using AutoMapper;
using Basket.API.Repositories.Interfaces;
using Basket.Domain.Model;
using Basket.Services.Interfaces;
using Basket.Services.Models;
using Basket.Services.Models.ServiceDTOs;

namespace Basket.Services.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;
        private readonly IDiscountService _discountService;

        public BasketService(IBasketRepository basketRepository, IMapper mapper, IDiscountService discountService)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _discountService = discountService;
        }

        public async Task<ServiceResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return ServiceResult.Success;
        }

        public async Task<ServiceValueResult<ShoppingCartDTO>> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);
            if (basket == null)
            {
                return ServiceValueResult<ShoppingCartDTO>.Failed("Basker not found");
            }

            return ServiceValueResult<ShoppingCartDTO>.Success(_mapper.Map<ShoppingCartDTO>(basket));
        }

        public async Task<ServiceValueResult<ShoppingCartDTO>> UpdateBasket(ShoppingCartDTO basket)
        {
            foreach (var item in basket.Items)
            {
                var discountResult = await _discountService.GetDiscount(item.ProductName);

                if (discountResult.Succeeded)
                {
                    item.Price -= discountResult.Model.Amount;
                }
            }

            var updatedBasket = await _basketRepository.UpdateBasket(_mapper.Map<ShoppingCart>(basket));
            if (updatedBasket == null)
            {
                return ServiceValueResult<ShoppingCartDTO>.Failed("Basket not updated");
            }

            return ServiceValueResult<ShoppingCartDTO>.Success(_mapper.Map<ShoppingCartDTO>(updatedBasket));
        }
    }
}
