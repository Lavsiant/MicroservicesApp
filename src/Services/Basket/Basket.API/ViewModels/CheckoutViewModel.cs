namespace Basket.API.ViewModels
{
    public class CheckoutViewModel
    {
        public string Username { get; set; }
        public long TotalCost { get; set; }
        public AddressViewModel BillingAddress { get; set; }
        public CardViewModel PaymentCard { get; set; }
    }

    public class AddressViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class CardViewModel
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Expiration { get; set; }
        public string CVV { get; set; }
    }
}
