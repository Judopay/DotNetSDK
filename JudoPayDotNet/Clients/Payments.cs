using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class Payments : JudoPayClient, IPayments
    {
        private const string ADDRESS = "";

        public Payments(IClient client) : base(client, ADDRESS)
        {
        }

        public Task<IResult<PaymentReceiptModel>> Create(CardPaymentModel cardPayment)
        {
            return CreateInternal<CardPaymentModel, PaymentReceiptModel>(cardPayment);
        }

        public Task<IResult<PaymentReceiptModel>> Create(TokenPaymentModel tokenPayment)
        {
            return CreateInternal<TokenPaymentModel, PaymentReceiptModel>(tokenPayment);
        }
    }
}
