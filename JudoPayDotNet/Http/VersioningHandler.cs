using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
	/// <summary>
	/// The JudoPay API supports multiple api versions, this handler adds the "API-Version" header to requests
	/// </summary>
	internal class VersioningHandler : DelegatingHandler
    {
        private readonly string _apiVersionHeader;
        private readonly string _apiVersionValue;

        public VersioningHandler(string apiVersionHeader, string apiVersionValue)
        {
            _apiVersionHeader = apiVersionHeader;
            _apiVersionValue = apiVersionValue;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(_apiVersionHeader, _apiVersionValue);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
