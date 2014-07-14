using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class Payments : JudoPayClient, IPayments
    {
        private const string CREATEADDRESS = "transactions/payments";
        private const string VALIDATEADDRESS = "transactions/payments/validate";

        public Payments(IClient client) : base(client)
        {
        }

        public Task<IResult<PaymentReceiptModel>> Create(CardPaymentModel cardPayment)
        {
            return PostInternal<CardPaymentModel, PaymentReceiptModel>(CREATEADDRESS, cardPayment);
        }

        public Task<IResult<PaymentReceiptModel>> Create(TokenPaymentModel tokenPayment)
        {
            return PostInternal<TokenPaymentModel, PaymentReceiptModel>(CREATEADDRESS, tokenPayment);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPayment)
        {
            return PostInternal<CardPaymentModel, JudoApiErrorModel>(VALIDATEADDRESS, cardPayment);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPayment)
        {
            return PostInternal<TokenPaymentModel, JudoApiErrorModel>(VALIDATEADDRESS, tokenPayment);
        }
    }
}
