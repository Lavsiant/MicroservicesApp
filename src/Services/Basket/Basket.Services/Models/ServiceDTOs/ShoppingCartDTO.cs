using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Services.Models.ServiceDTOs
{
    public class ShoppingCartDTO
    {
        public string Username { get; set; }

        public List<ShoppingCartItemDTO> Items { get; set; } = new List<ShoppingCartItemDTO>();

        public decimal TotalPrice
        {
            get
            {
                decimal totalprice = 0;
                foreach (var item in Items)
                {
                    totalprice += item.Price * item.Quantity;
                }
                return totalprice;
            }
        }
    }
}
