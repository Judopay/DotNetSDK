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
using JudoPayDotNetDotNet.Logging;
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
                    yield return new TestCaseData(new CardPaymentModel
                    {
                        Amount = 2.0m,
                        CardAddress = new CardAddressModel
                        {
                            Line1 = "Test Street",
                            PostCode = "W40 9AU",
                            Town = "Town"
                        },
                        CardNumber = "348417606737499",
                        ExpiryDate = "120615",
                        YourConsumerReference = "User10",
                        CV2 = "420",
                        JudoId = "14562",
                        MobileNumber = "07745352515",
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
                            "134567").SetName("RegisterCardWithSuccess");
                }
            }

            public static IEnumerable FailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new CardPaymentModel
                    {
                        Amount = 2.0m,
                        CardAddress = new CardAddressModel
                        {
                            Line1 = "Test Street",
                            PostCode = "W40 9AU",
                            Town = "Town"
                        },
                        CardNumber = "348417606737499",
                        ExpiryDate = "120615",
                        YourConsumerReference = "User10",
                        JudoId = "14562",
                        MobileNumber = "07745352515",
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


        [Test, TestCaseSource(typeof(global::JudoPayDotNetTests.Clients.RegisterCardsTests.RegisterCardsTestSource), "SuccessTestCases")]
        public void RegisterCardWithSuccess(CardPaymentModel registerCard, string responseData, string receiptId)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://judo.com"));

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

        [Test, TestCaseSource(typeof(global::JudoPayDotNetTests.Clients.RegisterCardsTests.RegisterCardsTestSource), "FailureTestCases")]
        public void RegisterCardWithError(CardPaymentModel registerCard, string responseData, JudoApiError errorType)
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

            IResult<ITransactionResult> paymentReceiptResult = null;

            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            paymentReceiptResult = judo.RegisterCards.Create(registerCard).Result;
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual(errorType, paymentReceiptResult.Error.Code);
        }
    }
}


