using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class PreAuths : JudoPayClient, IPreAuths
    {
        private const string ADDRESS = "";

        public PreAuths(IClient client)
            : base(client, ADDRESS)
        {
        }

        public Task<IResult<PaymentReceiptModel>> Create(CardPaymentModel cardPreAuth)
        {
            return CreateInternal<CardPaymentModel, PaymentReceiptModel>(cardPreAuth);
        }

        public Task<IResult<PaymentReceiptModel>> Create(TokenPaymentModel tokenPreAuth)
        {
            return CreateInternal<TokenPaymentModel, PaymentReceiptModel>(tokenPreAuth);
        }
    }
}
