using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using JudoPayDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients.WebPayments
{
    [TestFixture]
    public class PaymentsTests
    {
        [Test]
        public void CreatePayment()
        {
            var httpClient = Substitute.For<IHttpClient>();
            var request = new WebPaymentRequestModel
            {
                Amount = 10,
                CardAddress = new WebPaymentCardAddress
                {
                    CardHolderName = "Test User",
                    Address1 = "Test Street",
                    Address2 = "Test Street",
                    Address3 = "Test Street",
                    Town = "London",
                    PostCode = "W31 4HS",
                    CountryCode = 826
                },
                Currency = "GBP",
                ExpiryDate = DateTimeOffset.Now,
                JudoId = "1254634",
                CancelUrl = "https://www.test.com",
                SuccessUrl = "https://www.test.com",
                YourConsumerReference = "4235325"
            };

            const string EXTRA_HEADER_NAME = "ExtraHeader";

            request.HttpHeaders.Add(EXTRA_HEADER_NAME, "ExtraHeaderValue");

            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(@"{
		                                             postUrl : 'http://test.com',
		                                             reference : '1342423'   
                                                }")};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Is<HttpRequestMessage>(r => r.Headers.Contains(EXTRA_HEADER_NAME)))
                .Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.WebPayments.Payments.Create(request).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.AreEqual("1342423", paymentReceiptResult.Response.Reference);
            Assert.NotNull(paymentReceiptResult.Response.PostUrl);
        }
    }
}
