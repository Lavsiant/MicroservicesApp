using Basket.Services.Models;
using Basket.Services.Models.ServiceDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<ServiceValueResult<CouponDTO>> GetDiscount(string productName);
    }
}
