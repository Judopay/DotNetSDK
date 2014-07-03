using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class Payments : JudoPayClient, IPayments
    {
        private const string ADDRESS = "";

        public Payments(IClient client) : base(client, ADDRESS)
        {
        }

        private async Task<IResult<PaymentReceiptModel>> CreatePayment(PaymentModel payment)
        {
            PaymentReceiptModel result = null;

            var response = await Client.Post<PaymentReceiptModel>(Address, body: payment).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<PaymentReceiptModel>(result, response.JudoError);
        }

        public Task<IResult<PaymentReceiptModel>> Create(CardPaymentModel cardPayment)
        {
            return CreatePayment(cardPayment);
        }

        public Task<IResult<PaymentReceiptModel>> Create(TokenPaymentModel tokenPayment)
        {
            return CreatePayment(tokenPayment);
        }
    }
}
