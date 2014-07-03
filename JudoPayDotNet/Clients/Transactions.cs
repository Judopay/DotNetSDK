using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    internal class Transactions : JudoPayClient, ITransactions
    {
        public Transactions(IClient client) : base(client)
        {
        }

        public TransactionModel Get()
        {
            return null;
        }
    }
}
