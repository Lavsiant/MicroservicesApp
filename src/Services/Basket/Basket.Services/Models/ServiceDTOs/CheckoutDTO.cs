using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Services.Models.ServiceDTOs
{
    public class CheckoutDTO
    {
        public string Username { get; set; }
        public long TotalCost { get; set; }
        public AddressDTO BillingAddress { get; set; }
        public CardDTO PaymentCard { get; set; }
    }

    public class AddressDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class CardDTO
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
    }
}
