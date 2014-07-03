namespace JudoPayDotNet.Models
{
    public class TokenPaymentModel : PaymentModel
    {
        public string ConsumerToken { get; set; }
        public string CardToken { get; set; }
    }
}