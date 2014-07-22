using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    public class VersioningHandler : DelegatingHandler
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
