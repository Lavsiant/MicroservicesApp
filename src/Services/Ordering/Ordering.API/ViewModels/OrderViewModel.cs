using Ordering.Domain.ReadModel.Model;

namespace Ordering.API.ViewModels
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public long TotalCost { get; set; }
        public OrderStatus Status { get; set; }
        public AddressViewModel BillingAddress { get; set; }
        public CardViewModel PaymentCard { get; set; }
    }
}
