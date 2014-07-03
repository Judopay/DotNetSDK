using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Client;
using JudoPayDotNet.Models;

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

        protected async Task<IResult<R>> CreateInternal<T,R>(T payment) where T : class
                                                                where R : class
        {
            R result = null;

            var response = await Client.Post<R>(Address, body: payment).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<R>(result, response.JudoError);
        }
    }
}
