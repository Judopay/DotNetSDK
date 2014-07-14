using System.Collections.Generic;
using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal class ThreeDs : JudoPayClient, IThreeDs
    {
        private const string GetThreeDAuthorizationAddress = "";
        private const string CompleteThreeDAuthorizationAddress = "";

        public ThreeDs(IClient client)
            : base(client)
        {
        }

        public Task<IResult<PaymentRequiresThreeDSecureModel>> GetThreeDAuthorization(string md)
        {
            var address = string.Format("{0}/{1}", GetThreeDAuthorizationAddress, md);

            return GetInternal<PaymentRequiresThreeDSecureModel>(address, 
                new Dictionary<string, string>() {{"md", md}});
        }

        public Task<IResult<PaymentReceiptModel>> Complete3DSecure(string receiptId, ThreeDResultModel model)
        {
            var address = string.Format("{0}/{1}", CompleteThreeDAuthorizationAddress, receiptId);

            return PutInternal<ThreeDResultModel, PaymentReceiptModel>(address, model);
        }
    }
}