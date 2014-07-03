using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Client;
using JudoPayDotNet.Http;

namespace JudoPayDotNet
{
    public class JudoPayments : IJudoPayments
    {
        public IPayments Payments { get; set; }
        public IRefunds Refunds { get; set; }
        public IPreAuths PreAuths { get; set; }
        public ITransactions Transactions { get; set; }

        public IClient Client { get; set; }
        public ICredentials Credentials { get; set; }

        public JudoPayments(string token, string secret, string address) : 
                                this(new Credentials(token, secret),  
                                     new Client.Client(new Connection(new HttpClientWrapper(), address))) { }

        public JudoPayments(ICredentials credentials, IClient client)
        {
            Client = client;
            Credentials = credentials;

            Payments = new Payments(client);
            Refunds = new Refunds(client);
            PreAuths = new PreAuths(client);
            Transactions = new Transactions(client);
        }

    }
}
