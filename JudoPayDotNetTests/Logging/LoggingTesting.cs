using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNetDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Logging
{
    [TestFixture]
    public class LoggingTesting
    {
        [Test]
        public void ConnectionLogErrorTest()
        {
            var httpClient = Substitute.For<IHttpClient>();
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent("test");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/text");

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(Task.FromResult(response));

            Connection connection = new Connection(httpClient, 
                                                    DotNetLoggerFactory.Create(typeof(LoggingTesting)),
                                                    "http://dummy");

            connection.Send<string>(HttpMethod.Get, "test");
        }
    }
}
