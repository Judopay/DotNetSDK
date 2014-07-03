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
    internal class PreAuths : JudoPayClient, IPreAuths
    {
        public PreAuths(IClient client) : base(client)
        {
        }

        public ITransactionResult Create()
        {
            
        }

        public void Collected()
        {
            
        }
    }
}
