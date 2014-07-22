using System;
using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients.Merchant
{
    internal class Merchants : JudoPayClient, IMerchants
    {
        private const string Getaddress = "merchants";

        public Merchants(ILog logger, IClient client) : base(logger, client)
        {
        }

        public Task<IResult<MerchantModel>> Get(string judoId)
        {
            var address = String.Format("{0}/{1}", Getaddress, judoId);

            return GetInternal<MerchantModel>(address);
        }
    }
}
