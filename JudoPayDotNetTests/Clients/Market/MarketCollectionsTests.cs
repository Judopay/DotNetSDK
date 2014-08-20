using System.Collections;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients.Market
{
    [TestFixture]
    public class MarketCollectionsTests
    {
        //Test data
        private class MarketCollectionsTestSource
        {
            public static IEnumerable CreateSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new CollectionModel
                    {
                        Amount = 2.0m,
                        ReceiptId = 34560,
                        YourPaymentReference = "Pay1234"
                    },
                        @"{
                            receiptId : '134567',
                            type : 'Create',
                            judoId : '12456',
                            originalAmount : 20,
                            amount : 20,
                            netAmount : 20,
                            cardDetails :
                                {
                                    cardLastfour : '1345',
                                    endDate : '1214',
                                    cardToken : 'ASb345AE',
                                    cardType : 'VISA'
                                },
                            currency : 'GBP',
                            consumer : 
                                {
                                    consumerToken : 'B245SEB',
                                    yourConsumerReference : 'Consumer1'
                                }
                            }",
                        "134567").SetName("CollectionWithSuccess");
                }
            }

            public static IEnumerable CreateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new CollectionModel
                    {
                        Amount = 2.0m,
                        ReceiptId = 34560,
                        YourPaymentReference = "Pay1234"
                    },
                        @"    
                        {
                            errorMessage : 'Payment not made',
                            modelErrors : [{
                                            fieldName : 'receiptId',
                                            errorMessage : 'To large',
                                            detailErrorMessage : 'This field has to be at most 20 characters'
                                          }],
                            errorType : '11'
                        }",
                        JudoApiError.Payment_Declined).SetName("CollectionWithoutSuccess");
                }
            }
        }


        [Test, TestCaseSource(typeof(MarketCollectionsTestSource), "CreateSuccessTestCases")]
        public void CollectionWithSuccess(CollectionModel collections, string responseData, string receiptId)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(responseData)};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://judo.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.Market.Collections.Create(collections).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.AreEqual("134567", paymentReceiptResult.Response.ReceiptId);
        }

        [Test, TestCaseSource(typeof(MarketCollectionsTestSource), "CreateFailureTestCases")]
        public void CollectionWithError(CollectionModel collections, string responseData, JudoApiError error)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(responseData)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://judo.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.Market.Collections.Create(collections).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual(error, paymentReceiptResult.Error.ErrorType);
        }
    }
}
