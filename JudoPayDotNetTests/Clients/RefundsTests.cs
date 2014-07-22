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

namespace JudoPayDotNetTests.Clients
{
    [TestFixture]
    public class RefundsTests
    {
        //Test data
        public class RefundsTestSource
        {
            public static IEnumerable SuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new RefundModel
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
                        "134567").SetName("RefundWithSuccess");
                }
            }

            public static IEnumerable FailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new RefundModel
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
                            errorType : '200'
                        }",
                        200).SetName("PayWithCardWithoutSuccess");
                }
            }

            public static IEnumerable ValidateSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new RefundModel
                    {
                        Amount = 2.0m,
                        ReceiptId = 34560,
                        YourPaymentReference = "Pay1234"
                    },
                        @"{
                            errorMessage : 'Your good to go!',
                            errorType : '20'
                        }",
                        20).SetName("CollectionWithSuccess");
                }
            }

            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new RefundModel
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
                            errorType : '200'
                        }",
                        200).SetName("CollectionWithoutSuccess");
                }
            }
        }


        [Test, TestCaseSource(typeof(RefundsTestSource), "SuccessTestCases")]
        public void RefundWithSuccess(RefundModel refund, string responseData, string receiptId)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(responseData)};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient, 
                                                    DotNetLoggerFactory.Create(typeof(Connection)), 
                                                    "http://judo.com"));

            var judo = new JudoPayments(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.Refunds.Create(refund).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.AreEqual(paymentReceiptResult.Response.ReceiptId, "134567");
        }

        [Test, TestCaseSource(typeof(RefundsTestSource), "FailureTestCases")]
        public void RefundWithError(RefundModel refund, string responseData, JudoApiError error)
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
                                                    DotNetLoggerFactory.Create(typeof(Connection)), 
                                                    "http://judo.com"));

            var judo = new JudoPayments(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.Refunds.Create(refund).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual(error, paymentReceiptResult.Error.ErrorType);
        }

        [Test, TestCaseSource(typeof(RefundsTestSource), "ValidateSuccessTestCases")]
        public void ValidateWithSuccess(RefundModel refund, string responseData, JudoApiError errorType)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(responseData)};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create(typeof(Connection)),
                                                    "http://judo.com"));

            var judo = new JudoPayments(DotNetLoggerFactory.Create, client);

            var collectionValidationResult = judo.Refunds.Validate(refund).Result;

            Assert.NotNull(collectionValidationResult);
            Assert.IsFalse(collectionValidationResult.HasError);
            Assert.NotNull(collectionValidationResult.Response);
            Assert.AreEqual(errorType, collectionValidationResult.Response.ErrorType);
        }

        [Test, TestCaseSource(typeof(RefundsTestSource), "ValidateFailureTestCases")]
        public void ValidateWithoutSuccess(RefundModel refund, string responseData, JudoApiError errorType)
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
                                                    DotNetLoggerFactory.Create(typeof(Connection)),
                                                    "http://judo.com"));

            var judo = new JudoPayments(DotNetLoggerFactory.Create, client);

            var collectionValidationResult = judo.Refunds.Validate(refund).Result;

            Assert.NotNull(collectionValidationResult);
            Assert.IsTrue(collectionValidationResult.HasError);
            Assert.IsNull(collectionValidationResult.Response);
            Assert.IsNotNull(collectionValidationResult.Error);
            Assert.AreEqual(errorType, collectionValidationResult.Error.ErrorType);
        }
        
    }
}