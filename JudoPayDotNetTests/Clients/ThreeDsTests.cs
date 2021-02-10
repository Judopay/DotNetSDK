using System.Collections;
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

namespace JudoPayDotNetTests.Clients
{
    [TestFixture]
    public class ThreeDsTests
    {
        //Test data
        private class ThreeDCaseSources
        {
            public static IEnumerable GetSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData("42353vd22",
                        @"{
                            receiptId : 134567,
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
                            message : 'ThreeDSecureNotSuccessful',
                            modelErrors : [{
                                            fieldName : 'receiptId',
                                            message : 'To large',
                                            detail : 'This field has to be at most 20 characters',
                                            code : '0'
                                          }],
                            code : '9',
                            category : '0'
                        }").SetName("GetThreeDsAuthorizationsWithoutSuccess");
                }
            }

            public static IEnumerable CompleteSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(4235322,
                        new ThreeDResultModel
                        {
                            PaRes = "134253623AbE3442"
                        },
                         @"{
                            receiptId : 134567,
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
                    yield return new TestCaseData(4235322,
                        new ThreeDResultModel
                        {
                            PaRes = "134253623AbE3442"
                        },
//                        @"    
//                        {
//                            errorMessage : 'ThreeDSecureNotSuccessful',
//                            errorType : '9'
//                        }"
                          @"    
                        {
                            message : 'ThreeDSecureNotSuccessful',
                            modelErrors : [{
                                            fieldName : 'receiptId',
                                            message : 'To large',
                                            detail : 'This field has to be at most 20 characters',
                                            code : '0'
                                          }],
                            code : '9',
                            category : '0'
                        }"
                        ).SetName("CompleteThreeDsAuthorizationsWithoutSuccess");
                }
            }
        }

        [Test, TestCaseSource(typeof(ThreeDCaseSources), "CompleteSuccessTestCases")]
        public void CompleteThreeDsWithSuccess(long receiptId, ThreeDResultModel threeDResult, 
                                                string responseData)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(responseData)};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult =
                                judo.ThreeDs.Complete3DSecure(receiptId, threeDResult).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.ReceiptId, Is.EqualTo(134567));
            Assert.That(paymentReceiptResult.Response.ThreeDSecure.Result, Is.EqualTo("done"));
        }

        [Test, TestCaseSource(typeof(ThreeDCaseSources), "CompleteFailureTestCases")]
        public void CompleteThreeDsFail(long receiptId, ThreeDResultModel threeDResult,
                                                string responseData)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(responseData)
            };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult =
                                judo.ThreeDs.Complete3DSecure(receiptId, threeDResult).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
        }
    }
}
