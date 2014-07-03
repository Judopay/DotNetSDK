using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Client;

namespace JudoPayDotNet.Clients
{
    internal abstract class JudoPayClient
    {
        protected readonly IClient Client;
        protected readonly string Address;

        protected JudoPayClient(IClient client, string address)
        {
            Client = client;
            Address = address;
        }
    }
}
