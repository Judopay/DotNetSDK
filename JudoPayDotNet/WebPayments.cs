using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Clients.WebPayments;

namespace JudoPayDotNet
{
    internal class WebPayments : IWebPayments
    {
        public IPayments Payments { get; set; }

        public IPreAuths PreAuths { get; set; }

        public ITransactions Transactions { get; set; }
    }
}
