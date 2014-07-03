namespace JudoPayDotNet.Models
{
    public class CardPaymentModel : PaymentModel
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public CardAddressModel CardAddress { get; set; }
    }
}