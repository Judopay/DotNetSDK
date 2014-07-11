using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    /// <summary>
    /// This client is a simple wrapper of IHttpClient
    /// </summary>
    public class HttpClientWrapper : IHttpClient
    {
        public readonly HttpClient HttpClient;

        public HttpClientWrapper()
        {
            HttpClient = createHttpClient();
        }

        public HttpClientWrapper(DelegatingHandler handler)
        {
            HttpClient = createHttpClient(handler);
        }

        public HttpClientWrapper(params DelegatingHandler[] handlers)
        {
            HttpClient = createHttpClient(handlers);
        }

        private HttpClient createHttpClient(params DelegatingHandler[] handlers)
        {
            if (!handlers.Any())
            {
                return new HttpClient();
            }

            if (handlers.Count() == 1)
            {
                return new HttpClient(handlers.First());
            }

            var firstHandler = handlers.First();
            var previousHandler = firstHandler;

            for (int i = 1; i < handlers.Length; ++i)
            {
                var currentHandler = handlers[i];

                previousHandler.InnerHandler = currentHandler;

                previousHandler = currentHandler;
            }

            return new HttpClient(firstHandler);
        }

        public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return HttpClient.SendAsync(request);
        }
    }
}
