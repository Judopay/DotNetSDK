using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;

namespace JudoPayDotNet
{
    internal class Refunds : JudoPayClient, IRefunds
    {
        public Refunds(IClient client) : base(client)
        {
        }

        public void Refund()
        {
            
        }
    }
}
