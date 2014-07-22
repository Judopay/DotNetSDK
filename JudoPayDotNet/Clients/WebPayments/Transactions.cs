using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.WebPayments
{
    internal class Transactions : JudoPayClient, ITransactions
    {
        private const string BASEADDRESS = "webpayments";
        private const string BASEADDRESSGETBYRECEIPT = "transactions";

        public Transactions(ILog logger, IClient client) : base(logger, client)
        {
        }

        public Task<IResult<WebPaymentRequestModel>> Get(string reference)
        {
            var address = string.Format("{0}/{1}", BASEADDRESS, reference);

            return GetInternal<WebPaymentRequestModel>(address);
        }

        public Task<IResult<WebPaymentRequestModel>> Get(string reference, TransactionType type)
        {

            if (type == TransactionType.PREAUTH || type == TransactionType.SALE)
            {
                var address = string.Format("{0}/{1}/{2}", BASEADDRESS, type, reference);

                return GetInternal<WebPaymentRequestModel>(address);
            }

            //Transactions can only be fetched by transation types SALE or REFUND
            return Task.FromResult<IResult<WebPaymentRequestModel>>(new Result<WebPaymentRequestModel>(null,
                new JudoApiErrorModel()
                {
                    ErrorMessage = "Wrong transaction type used",
                    ErrorType = JudoApiError.General_Error
                }));
        }

        public Task<IResult<WebPaymentRequestModel>> GetByReceipt(string receiptId)
        {
            var address = string.Format("{0}/{1}/webpayment", BASEADDRESSGETBYRECEIPT, receiptId);

            return GetInternal<WebPaymentRequestModel>(address);
        }
    }
}
