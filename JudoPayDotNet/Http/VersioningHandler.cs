using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    using System.Reflection;

    /// <summary>
    /// The JudoPay API supports multiple api versions, this handler adds the "API-Version" header to requests
    /// </summary>
    internal class VersioningHandler : DelegatingHandler
    {
        public const string API_VERSION_HEADER = "api-version";

        internal const string DEFAULT_API_VERSION = "5.2.0.0";

        internal const string SDK_VERSION_HEADER = "sdk-version";

        private readonly string _apiVersionValue;

        internal static string SdkVersionDetails = "DotNetSDK-" + new AssemblyName(typeof(HttpClientWrapper).GetTypeInfo().Assembly.FullName).Version;

        private readonly string _clientVersion = SdkVersionDetails;

        public VersioningHandler(string apiVersionValue, string clientVersion)
        {
            _apiVersionValue = apiVersionValue;
            if (!string.IsNullOrWhiteSpace(clientVersion))
            {
                this._clientVersion += ";" + clientVersion;
            }
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add(SDK_VERSION_HEADER, this._clientVersion);
            request.Headers.Add(API_VERSION_HEADER, _apiVersionValue);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
