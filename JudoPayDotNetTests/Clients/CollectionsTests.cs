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
    public class CollectionsTests
    {
        //Test data
        private class CollectionsTestSource
        {
            public static IEnumerable CreateSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new CollectionModel
                    {
                        Amount = 2.0m,
                        ReceiptId = 34560,
                        
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
                        
                    },

                            @"    
                        {
                            message : 'Payment not made',
                            modelErrors : [{
                                            fieldName : 'receiptId',
                                            message : 'To large',
                                            detail : 'This field has to be at most 20 characters',
                                            code : '0'
                                          }],
                            code : '11',
                            category : '0'
                        }",
                        JudoApiError.Payment_Declined).SetName("CollectionWithoutSuccess");
                }
            }

            public static IEnumerable ValidateSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new CollectionModel
                    {
                        Amount = 2.0m,
                        ReceiptId = 34560,
                        
                    },
                        @"{
                            errorMessage : 'Your good to go!',
                            errorType : '20'
                        }",
                        JudoApiError.Validation_Passed).SetName("ValidationWithSuccess");
                }
            }

            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new CollectionModel
                    {
                        Amount = 2.0m,
                        ReceiptId = 34560,
                        
                    },
                         @"    
                        {
                            message : 'Payment not made',
                            modelErrors : [{
                                            fieldName : 'receiptId',
                                            message : 'To large',
                                            detail : 'This field has to be at most 20 characters',
                                            code : '0'
                                          }],
                            code : '11',
                            category : '0'
                        }",
                        JudoApiError.Payment_Declined).SetName("ValidationWithoutSuccess");
                }
            }
        }


        [Test, TestCaseSource(typeof(CollectionsTestSource), "CreateSuccessTestCases")]
        public void CollectionWithSuccess(CollectionModel collections, string responseData, string receiptId)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.Collections.Create(collections).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.ReceiptId, Is.EqualTo(134567));
        }

        [Test, TestCaseSource(typeof(CollectionsTestSource), "CreateFailureTestCases")]
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
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.Collections.Create(collections).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual((int)error, paymentReceiptResult.Error.Code);
        }

        [Test, TestCaseSource(typeof(CollectionsTestSource), "ValidateSuccessTestCases")]
        public void ValidateWithSuccess(CollectionModel collection, string responseData, JudoApiError errorType)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var collectionValidationResult = judo.Collections.Validate(collection).Result;

            Assert.NotNull(collectionValidationResult);
            Assert.IsFalse(collectionValidationResult.HasError);
            Assert.NotNull(collectionValidationResult.Response);
            Assert.AreEqual(errorType, collectionValidationResult.Response.ErrorType);
        }

        [Test, TestCaseSource(typeof(CollectionsTestSource), "ValidateFailureTestCases")]
        public void ValidateWithoutSuccess(CollectionModel collection, string responseData, JudoApiError errorType)
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
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var collectionValidationResult = judo.Collections.Validate(collection).Result;

            Assert.NotNull(collectionValidationResult);
            Assert.IsTrue(collectionValidationResult.HasError);
            Assert.IsNull(collectionValidationResult.Response);
            Assert.IsNotNull(collectionValidationResult.Error);
            Assert.AreEqual((int)errorType, collectionValidationResult.Error.Code);
        }
    }
}
