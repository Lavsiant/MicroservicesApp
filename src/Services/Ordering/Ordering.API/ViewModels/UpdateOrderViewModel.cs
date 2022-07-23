namespace Ordering.API.ViewModels
{
    public class UpdateOrderViewModel
    {
        public string Id { get; set; }
        public long TotalCost { get; set; }
        public AddressViewModel BillingAddress { get; set; }
        public CardViewModel PaymentCard { get; set; }
    }
}
