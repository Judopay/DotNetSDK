using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JudoPayDotNet.Client
{
    internal class Handler : HttpMessageHandler 
    {
        //TODO: in here should be implemented all the authentication and signing of requests

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            // Send request

            // Parse response

            // Handle errors

            throw new NotImplementedException();
        }
    }
}
