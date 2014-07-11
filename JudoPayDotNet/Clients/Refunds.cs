using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class Refunds : JudoPayClient, IRefunds
    {
        private const string ADDRESS = "";

        public Refunds(IClient client) : base(client, ADDRESS)
        {
        }

        public Task<IResult<PaymentReceiptModel>> Create(RefundModel refund)
        {
            return CreateInternal<RefundModel, PaymentReceiptModel>(refund);
        }
    }
}
