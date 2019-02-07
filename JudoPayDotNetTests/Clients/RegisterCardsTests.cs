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
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using JudoPayDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients
{

    [TestFixture]
    public class RegisterCardsTests
    {
        //Test data
        private class RegisterCardsTestSource
        {
            public static IEnumerable SuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new RegisterCardModel
                    {
                        CardAddress = new CardAddressModel
                        {
                            Line1 = "Test Street",
                            PostCode = "W40 9AU",
                            Town = "Town"
                        },
                        CardNumber = "348417606737499",
                        ExpiryDate = "120615",
                        YourConsumerReference = "User10",
                        CV2 = "420"
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
                            "134567").SetName("RegisterCardWithSuccess");
                }
            }

            public static IEnumerable FailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new RegisterCardModel
                    {
                        CardAddress = new CardAddressModel
                        {
                            Line1 = "Test Street",
                            PostCode = "W40 9AU",
                            Town = "Town"
                        },
                        CardNumber = "348417606737499",
                        ExpiryDate = "120615",
                        YourConsumerReference = "User10"
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
                            code : '1',
                            category : '0'
                        }",
                        1).SetName("RegisterCardWithoutSuccess");
                }
            }

            public static IEnumerable ValidateSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new RegisterCardModel
                    {
                        CardAddress = new CardAddressModel
                        {
                            Line1 = "Test Street",
                            PostCode = "W40 9AU",
                            Town = "Town"
                        },
                        CardNumber = "348417606737499",
                        ExpiryDate = "120615",
                        YourConsumerReference = "User10",
                    },
                        @"{
                        errorMessage : 'Your good to go!',
                        errorType : '20'
                    }",
                            20).SetName("ValidateSuccess");
                }
            }

            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new RegisterCardModel
                    {
                        CardAddress = new CardAddressModel
                        {
                            Line1 = "Test Street",
                            PostCode = "W40 9AU",
                            Town = "Town"
                        },
                        CardNumber = "348417606737499",
                        ExpiryDate = "120615",
                        YourConsumerReference = "User10",
                    },
                            @"    
                    {
                        errorMessage : 'Payment not made',
                        modelErrors : [{
                                        fieldName : 'receiptId',
                                        errorMessage : 'To large',
                                        detailErrorMessage : 'This field has to be at most 20 characters'
                                        }],
                        errorType : '1'
                    }",
                            1).SetName("ValidateWithoutSuccess");
                }
            }
        }


        [Test, TestCaseSource(typeof(RegisterCardsTestSource), nameof(RegisterCardsTestSource.SuccessTestCases))]
        public void RegisterCardWithSuccess(RegisterCardModel registerCard, string responseData, string receiptId)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(
                httpClient,
                DotNetLoggerFactory.Create,
                "http://something.com")
            );

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            IResult<ITransactionResult> paymentReceiptResult = null;

            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            paymentReceiptResult = judo.RegisterCards.Create(registerCard).Result;
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.ReceiptId, Is.EqualTo(134567));
        }

        [Test, TestCaseSource(typeof(RegisterCardsTestSource), nameof(RegisterCardsTestSource.SuccessTestCases))]
        public void ExtraHeadersAreSent(RegisterCardModel payment, string responseData, string receiptId)
        {
            const string EXTRA_HEADER_NAME = "X-Extra-Request-Header";

            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Is<HttpRequestMessage>(r => r.Headers.Contains(EXTRA_HEADER_NAME)))
                .Returns(responseTask.Task);

            var client = new Client(new Connection(
                httpClient,
                DotNetLoggerFactory.Create,
                "http://something.com")
            );

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            payment.HttpHeaders.Add(EXTRA_HEADER_NAME, "some random value");

             IResult<ITransactionResult> paymentReceiptResult = judo.RegisterCards.Create(payment).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.ReceiptId, Is.EqualTo(134567));
        }

        [Test, TestCaseSource(typeof(RegisterCardsTestSource), nameof(RegisterCardsTestSource.FailureTestCases))]
        public void RegisterCardWithError(RegisterCardModel registerCard, string responseData, JudoApiError errorType)
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

            var client = new Client(new Connection(
                httpClient,
                DotNetLoggerFactory.Create,
                "http://something.com")
            );

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            IResult<ITransactionResult> paymentReceiptResult = null;

            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            paymentReceiptResult = judo.RegisterCards.Create(registerCard).Result;
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual((int)errorType, paymentReceiptResult.Error.Code);
        }
    }
}


