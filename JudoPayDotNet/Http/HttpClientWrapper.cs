using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    /// <summary>
    /// This client is a simple wrapper of IHttpClient
    /// </summary>
    // ReSharper disable UnusedMember.Global
    internal class HttpClientWrapper : IHttpClient
    {
        public readonly HttpClient HttpClient;

        public HttpClientWrapper()
        {
            HttpClient = CreateHttpClient();
        }

        public HttpClientWrapper(DelegatingHandler handler)
        {
            HttpClient = CreateHttpClient(handler);
        }

        public HttpClientWrapper(params DelegatingHandler[] handlers)
        {
            HttpClient = CreateHttpClient(handlers);
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

            //This is the default delegating handler that actually does the http request
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
