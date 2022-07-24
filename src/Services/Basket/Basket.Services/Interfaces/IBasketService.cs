using Basket.Services.Models;
using Basket.Services.Models.ServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Services.Interfaces
{
    public interface IBasketService
    {
        Task<ServiceValueResult<ShoppingCartDTO>> GetBasket(string userName);
        Task<ServiceValueResult<ShoppingCartDTO>> UpdateBasket(ShoppingCartDTO basket);
        Task<ServiceValueResult<ShoppingCartDTO>> Checkout(CheckoutDTO model);
        Task<ServiceResult> DeleteBasket(string userName);
    }
}
