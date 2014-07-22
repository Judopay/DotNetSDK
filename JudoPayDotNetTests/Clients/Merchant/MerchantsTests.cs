using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Http;
using JudoPayDotNetDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients.Merchant
{
    [TestFixture]
    public class MerchantsTests
    {
        [Test]
        public void GetMerchantByJudoId()
        {

            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(@"{
    	                                                judoId : '12345',
    	                                                tradingName : 'testSubject',
    	                                                appearsOnStatementAs : 'merchant'
                                                    }")};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create(typeof(Connection)),
                                                    "http://judo.com"));

            var judo = new JudoPayments(DotNetLoggerFactory.Create, client);

            var result = judo.Merchants.Get("12345").Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.NotNull(result.Response);
            Assert.AreEqual("12345", result.Response.JudoId);
            Assert.AreEqual("merchant", result.Response.AppearsOnStatementAs);
        }
    }
}
