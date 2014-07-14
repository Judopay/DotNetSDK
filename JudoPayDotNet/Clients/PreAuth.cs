using System.Threading.Tasks;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class PreAuths : JudoPayClient, IPreAuths
    {
        private const string CREATEPREAUTHADDRESS = "transactions/preauths";
        private const string VALIDATEPREAUTHADDRESS = "transactions/preauths/validate";

        public PreAuths(IClient client)
            : base(client)
        {
        }

        public Task<IResult<PaymentReceiptModel>> Create(CardPaymentModel cardPreAuth)
        {
            return PostInternal<CardPaymentModel, PaymentReceiptModel>(CREATEPREAUTHADDRESS, cardPreAuth);
        }

        public Task<IResult<PaymentReceiptModel>> Create(TokenPaymentModel tokenPreAuth)
        {
            return PostInternal<TokenPaymentModel, PaymentReceiptModel>(CREATEPREAUTHADDRESS, tokenPreAuth);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(CardPaymentModel cardPayment)
        {
            return PostInternal<CardPaymentModel, JudoApiErrorModel>(VALIDATEPREAUTHADDRESS, cardPayment);
        }

        public Task<IResult<JudoApiErrorModel>> Validate(TokenPaymentModel tokenPayment)
        {
            return PostInternal<TokenPaymentModel, JudoApiErrorModel>(VALIDATEPREAUTHADDRESS, tokenPayment);
        }
    }
}
