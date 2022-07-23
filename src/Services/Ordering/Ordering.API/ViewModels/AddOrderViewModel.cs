using Ordering.Domain.ReadModel.Model;

namespace Ordering.API.ViewModels
{
    public class AddOrderViewModel
    {
        public string Username { get; set; }
        public long TotalCost { get; set; }
        public AddressViewModel BillingAddress { get; set; }
        public CardViewModel PaymentCard { get; set; }
    }
}
