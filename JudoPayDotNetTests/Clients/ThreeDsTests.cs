using System.Collections;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients
{
    [TestFixture]
    public class ThreeDsTests
    {
        //Test data
        public class ThreeDCaseSources
        {
            public static IEnumerable GetSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData("42353vd22",
                        @"{
                            receiptId : '134567',
                            result : 'the result',
                            message : 'a message',
                            acsUrl : '12456',
                            md : '1312453252352366647',
                            paReq : 'pa request',
                        }").SetName("GetThreeDsAuthorizationsWithSuccess");
                }
            }

            public static IEnumerable GetFailureTestCases
            {
                get
                {
                    yield return new TestCaseData("34254645gvdv3",
                        @"    
                        {
                            errorMessage : 'ThreeDSecureNotSuccessful',
                            errorType : '9'
                        }").SetName("GetThreeDsAuthorizationsWithoutSuccess");
                }
            }

            public static IEnumerable CompleteSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData("42353vd22",
                        new ThreeDResultModel()
                        {
                            PaRes = "134253623AbE3442"
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
                                },
                            threeDSecure :
                                {
                                    attempted : true,
                                    result : 'done' 
                                }
                            }").SetName("CompleteThreeDsAuthorizationsWithSuccess");
                }
            }

            public static IEnumerable CompleteFailureTestCases
            {
                get
                {
                    yield return new TestCaseData("42353vd22",
                        new ThreeDResultModel()
                        {
                            PaRes = "134253623AbE3442"
                        },@"    
                        {
                            errorMessage : 'ThreeDSecureNotSuccessful',
                            errorType : '9'
                        }").SetName("CompleteThreeDsAuthorizationsWithoutSuccess");
                }
            }
        }

        [Test, TestCaseSource(typeof(ThreeDCaseSources), "GetSuccessTestCases")]
        public void GetThreeDsAuthorizationsWithSuccess(string md, string responseData)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(responseData);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var credentials = new Credentials("ABC", "Secrete");
            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create(typeof(Connection)),
                                                    "http://judo.com"));

            JudoPayments judo = new JudoPayments(DotNetLoggerFactory.Create, credentials, client);

            IResult<PaymentRequiresThreeDSecureModel> paymentRequiresThreeD = 
                                            judo.ThreeDs.GetThreeDAuthorization(md).Result;

            Assert.NotNull(paymentRequiresThreeD);
            Assert.IsFalse(paymentRequiresThreeD.HasError);
            Assert.NotNull(paymentRequiresThreeD.Response);
            Assert.AreEqual(paymentRequiresThreeD.Response.ReceiptId, "134567");
        }

        [Test, TestCaseSource(typeof(ThreeDCaseSources), "GetFailureTestCases")]
        public void GetThreeDsAuthorizationsFail(string md, string responseData)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            response.Content = new StringContent(responseData);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var credentials = new Credentials("ABC", "Secrete");
            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create(typeof(Connection)),
                                                    "http://judo.com"));

            JudoPayments judo = new JudoPayments(DotNetLoggerFactory.Create, credentials, client);

            IResult<PaymentRequiresThreeDSecureModel> paymentReceiptResult = 
                                judo.ThreeDs.GetThreeDAuthorization(md).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
        }

        [Test, TestCaseSource(typeof(ThreeDCaseSources), "CompleteSuccessTestCases")]
        public void CompleteThreeDsWithSuccess(string receiptId, ThreeDResultModel threeDResult, 
                                                string responseData)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(responseData);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var credentials = new Credentials("ABC", "Secrete");
            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create(typeof(Connection)),
                                                    "http://judo.com"));

            JudoPayments judo = new JudoPayments(DotNetLoggerFactory.Create, credentials, client);

            IResult<PaymentReceiptModel> paymentReceiptResult =
                                judo.ThreeDs.Complete3DSecure(receiptId, threeDResult).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.AreEqual(paymentReceiptResult.Response.ReceiptId, "134567");
            Assert.AreEqual(paymentReceiptResult.Response.ThreeDSecure.Result, "done");
        }

        [Test, TestCaseSource(typeof(ThreeDCaseSources), "CompleteFailureTestCases")]
        public void CompleteThreeDsFail(string receiptId, ThreeDResultModel threeDResult,
                                                string responseData)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            response.Content = new StringContent(responseData);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var credentials = new Credentials("ABC", "Secrete");
            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create(typeof(Connection)),
                                                    "http://judo.com"));

            JudoPayments judo = new JudoPayments(DotNetLoggerFactory.Create, credentials, client);

            IResult<PaymentReceiptModel> paymentReceiptResult =
                                judo.ThreeDs.Complete3DSecure(receiptId, threeDResult).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
        }
    }
}
