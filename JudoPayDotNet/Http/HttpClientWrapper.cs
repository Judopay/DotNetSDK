using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    using System.Net.Http.Headers;
    using System.Reflection;

    using JudoPayDotNet.Clients;

    /// <summary>
    /// This client is a simple wrapper of IHttpClient
    /// </summary>
    // ReSharper disable UnusedMember.Global
    internal class HttpClientWrapper : IHttpClient
    {
        internal static readonly string SdkUserAgent = "DotNetSDK/" + new AssemblyName(typeof(HttpClientWrapper).GetTypeInfo().Assembly.FullName).Version;

        public readonly HttpClient HttpClient;

        public HttpClientWrapper()
        {
            HttpClient = CreateHttpClient();
        }

        public HttpClientWrapper(DelegatingHandler handler)
        {
            HttpClient = CreateHttpClient(handler);
        }

        public HttpClientWrapper(params DelegatingHandler[] handlers) : this(null, handlers)
        {
        }

        /// <summary>
        /// Initializes a new instance of the HttpWrapper class for senidng messages to the api
        /// </summary>
        /// <param name="userAgent">Details of the client calling the api, should be in the form PRODUCT/VERSION</param>
        /// <param name="handlers">a list of custom handlers</param>
        public HttpClientWrapper(ProductInfoHeaderValue userAgent, params DelegatingHandler[] handlers)
        {
            HttpClient = CreateHttpClient(handlers);
            HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd(SdkUserAgent);
            if (userAgent != null) HttpClient.DefaultRequestHeaders.UserAgent.Add(userAgent);
        }

        private static HttpClient CreateHttpClient(params DelegatingHandler[] handlers)
        {
            if (!handlers.Any())
            {
                return new HttpClient();
            }

            if (handlers.Count() == 1)
            {
                return new HttpClient(handlers.First());
            }

            DelegatingHandler currentHandler;
            var firstHandler = currentHandler = handlers.First();
            var previousHandler = firstHandler;

            for (var i = 1; i < handlers.Length; ++i)
            {
                currentHandler = handlers[i];

                previousHandler.InnerHandler = currentHandler;

                previousHandler = currentHandler;
            }

            // This is the default delegating handler that actually does the http request
            currentHandler.InnerHandler = new HttpClientHandler();

            return new HttpClient(firstHandler);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return HttpClient.SendAsync(request);
        }
    }
    // ReSharper restore UnusedMember.Global
}
