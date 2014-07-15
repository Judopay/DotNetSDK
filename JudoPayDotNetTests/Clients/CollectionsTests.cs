using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Client;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Errors;
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
        public class CollectionsTestSource
        {
            public static IEnumerable CreateSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new CollectionModel()
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
                    yield return new TestCaseData(new CollectionModel()
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

            public static IEnumerable ValidateSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new CollectionModel()
                    {
                        Amount = 2.0m,
                        ReceiptId = 34560,
                        YourPaymentReference = "Pay1234"
                    },
                        @"{
                            errorMessage : 'Your good to go!',
                            errorType : '20'
                        }",
                        JudoApiError.Validation_Passed).SetName("CollectionWithSuccess");
                }
            }

            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new CollectionModel()
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


        [Test, TestCaseSource(typeof (CollectionsTestSource), "CreateSuccessTestCases")]
        public void CollectionWithSuccess(CollectionModel collections, string responseData, string receiptId)
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

            JudoPayments judo = new JudoPayments(credentials, client);

            IResult<PaymentReceiptModel> paymentReceiptResult = judo.Collections.Create(collections).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.AreEqual("134567", paymentReceiptResult.Response.ReceiptId);
        }

        [Test, TestCaseSource(typeof (CollectionsTestSource), "CreateFailureTestCases")]
        public void CollectionWithError(CollectionModel collections, string responseData, JudoApiError error)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent(responseData);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var credentials = new Credentials("ABC", "Secrete");
            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create(typeof(Connection)), 
                                                    "http://judo.com"));

            JudoPayments judo = new JudoPayments(credentials, client);

            IResult<PaymentReceiptModel> paymentReceiptResult = judo.Collections.Create(collections).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual(error, paymentReceiptResult.Error.ErrorType);
        }

        [Test, TestCaseSource(typeof(CollectionsTestSource), "ValidateSuccessTestCases")]
        public void ValidateWithSuccess(CollectionModel collection, string responseData, JudoApiError errorType)
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

            JudoPayments judo = new JudoPayments(credentials, client);

            IResult<JudoApiErrorModel> collectionValidationResult = judo.Collections.Validate(collection).Result;

            Assert.NotNull(collectionValidationResult);
            Assert.IsFalse(collectionValidationResult.HasError);
            Assert.NotNull(collectionValidationResult.Response);
            Assert.AreEqual(errorType, collectionValidationResult.Response.ErrorType);
        }

        [Test, TestCaseSource(typeof(CollectionsTestSource), "ValidateFailureTestCases")]
        public void ValidateWithoutSuccess(CollectionModel collection, string responseData, JudoApiError errorType)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent(responseData);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var credentials = new Credentials("ABC", "Secrete");
            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create(typeof(Connection)),
                                                    "http://judo.com"));

            JudoPayments judo = new JudoPayments(credentials, client);

            IResult<JudoApiErrorModel> collectionValidationResult = judo.Collections.Validate(collection).Result;

            Assert.NotNull(collectionValidationResult);
            Assert.IsTrue(collectionValidationResult.HasError);
            Assert.IsNull(collectionValidationResult.Response);
            Assert.IsNotNull(collectionValidationResult.Error);
            Assert.AreEqual(errorType, collectionValidationResult.Error.ErrorType);
        }
    }
}
