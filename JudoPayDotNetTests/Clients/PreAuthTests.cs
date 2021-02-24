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
    public class PreAuthTests
    {
        //Test data
        private class PreAuthTestSource
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
                        ConsumerLocation = new ConsumerLocationModel
                        {
                            Latitude = 40m,
                            Longitude = 14m
                        },
                        CV2 = "420",
                        EmailAddress = "testaccount@judo.com",
                        ExpiryDate = "12/25",
                        JudoId = "100200300",
                        MobileNumber = "07999999999",
                        YourConsumerReference = "User10"
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
                            "134567").SetName("PreAuthWithCardWithSuccess");
                    yield return new TestCaseData(new TokenPaymentModel
                    {
                        Amount = 2.0m,
                        ConsumerLocation = new ConsumerLocationModel
                        {
                            Latitude = 40m,
                            Longitude = 14m
                        },
                        CV2 = "420",
                        CardToken = "A24BS2",
                        EmailAddress = "testaccount@judo.com",
                        JudoId = "100200300",
                        MobileNumber = "07999999999",
                        YourConsumerReference = "User10",
                       
                        ConsumerToken = "ABAS"
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
                            "134567").SetName("PreAuthWithTokenWithSuccess");
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
                        ConsumerLocation = new ConsumerLocationModel
                        {
                            Latitude = 40m,
                            Longitude = 14m
                        },
                        CV2 = "420",
                        EmailAddress = "testaccount@judo.com",
                        ExpiryDate = "12/25",
                        JudoId = "100200300",
                        MobileNumber = "07999999999",
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
                            code : '200',
                            category : '0'
                        }",
                        200).SetName("PreAuthWithCardWithoutSuccess");
                    yield return new TestCaseData(new TokenPaymentModel
                    {
                        Amount = 2.0m,
                        ConsumerLocation = new ConsumerLocationModel
                        {
                            Latitude = 40m,
                            Longitude = 14m
                        },
                        CV2 = "420",
                        CardToken = "A24BS2",
                        EmailAddress = "testaccount@judo.com",
                        JudoId = "100200300",
                        MobileNumber = "07999999999",
                        YourConsumerReference = "User10",
                        ConsumerToken = "ABAS"
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
                            code : '200',
                            category : '0'
                        }",
                        200).SetName("PreAuthWithTokenWithoutSuccess");
                }
            }

            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return
                        new TestCaseData(new CardPaymentModel
                        {
                            Amount = 2.0m,
                            CardAddress = new CardAddressModel { Line1 = "Test Street", PostCode = "W40 9AU", Town = "Town" },
                            CardNumber = "348417606737499",
                            ConsumerLocation = new ConsumerLocationModel { Latitude = 40m, Longitude = 14m },
                            CV2 = "420",
                            EmailAddress = "testaccount@judo.com",
                            ExpiryDate = "12/25",
                            JudoId = "100200300",
                            MobileNumber = "07999999999",
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
                            code : '12',
                            category : '0'
                        }",
                            JudoApiError.Payment_Failed).SetName("ValidateWithoutSuccess");
                    new TestCaseData(new TokenPaymentModel
                    {
                        Amount = 2.0m,
                        CardToken = "",
                        ConsumerLocation = new ConsumerLocationModel { Latitude = 40m, Longitude = 14m },
                        CV2 = "420",
                        EmailAddress = "testaccount@judo.com",
                        JudoId = "100200300",
                        MobileNumber = "07999999999",
                        YourConsumerReference = "User10"
                    },
                        @"
                        {
                            message : 'Sorry, we're unable to process your request. Please check your details and try again.',
                            modelErrors : [{
                                            fieldName : 'CardToken',
                                            message : 'Sorry, but for this transaction a card token must be supplied. Please check your details and try again.',
                                            detail : 'Sorry, we are unable to process your request at this time.',
                                            code : '52'
                                          }],
                            code : '1',
                            category : '2'
                        }",
                        JudoApiError.Payment_Failed).SetName("ValidateWithoutSuccess");
                    new TestCaseData(new OneTimePaymentModel
                    {
                        Amount = 2.0m,
                        OneUseToken = "",
                        ConsumerLocation = new ConsumerLocationModel { Latitude = 40m, Longitude = 14m },
                        EmailAddress = "testaccount@judo.com",
                        JudoId = "100200300",
                        MobileNumber = "07999999999",
                        YourConsumerReference = "User10"
                    },
                        @"
                        {
                            message : 'Sorry, we're unable to process your request. Please check your details and try again.',
                            modelErrors : [{
                                            fieldName : 'OneUseToken',
                                            message : 'Sorry, but for this transaction a card token must be supplied. Please check your details and try again.',
                                            detail : 'Sorry, we are unable to process your request at this time.',
                                            code : '970'
                                          }],
                            code : '1',
                            category : '2'
                        }",
                        JudoApiError.Payment_Failed).SetName("ValidateWithoutSuccess");
                    new TestCaseData(new PKPaymentModel
                    {
                        Amount = 2.0m,
                        PkPayment = new PKPaymentInnerModel
                        {
                            Token = new PKPaymentTokenModel
                            {
                                PaymentData = { }
                            }
                        },
                        ConsumerLocation = new ConsumerLocationModel { Latitude = 40m, Longitude = 14m },
                        JudoId = "100200300",
                        YourConsumerReference = "User10"
                    },
                        @"
                        {
                            message : 'We've been unable to decrypt the supplied Apple Pay token. Please check your API client configuration in the dashboard.',
                            code : '61',
                            category : '2'
                        }",
                        JudoApiError.Payment_Failed).SetName("ValidateWithoutSuccess");
                }
            }
        }


        [Test, TestCaseSource(typeof(PreAuthTestSource), "SuccessTestCases")]
        public void PreAuthWithSuccess(PaymentModel payment, string responseData, string receiptId)
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

            IResult<ITransactionResult> paymentReceiptResult = null;

            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            if (payment is CardPaymentModel)
            {
                paymentReceiptResult = judo.Payments.Create((CardPaymentModel)payment).Result;
            }
            else if (payment is TokenPaymentModel)
            {
                paymentReceiptResult = judo.Payments.Create((TokenPaymentModel)payment).Result;
            }
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.ReceiptId, Is.EqualTo(134567));
        }

        [Test, TestCaseSource(typeof(PreAuthTestSource), "SuccessTestCases")]
        public void ExtraHeadersAreSent(PaymentModel payment, string responseData, string receiptId)
        {
            const string EXTRA_HEADER_NAME = "X-Extra-Request-Header";

            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Is<HttpRequestMessage>(r => r.Headers.Contains(EXTRA_HEADER_NAME)))
                .Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            IResult<ITransactionResult> paymentReceiptResult = null;

            payment.HttpHeaders.Add(EXTRA_HEADER_NAME, "some random value");

            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            if (payment is CardPaymentModel)
            {
                paymentReceiptResult = judo.Payments.Create((CardPaymentModel)payment).Result;
            }
            else if (payment is TokenPaymentModel)
            {
                paymentReceiptResult = judo.Payments.Create((TokenPaymentModel)payment).Result;
            }
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.ReceiptId, Is.EqualTo(134567));
        }

        [Test, TestCaseSource(typeof(PreAuthTestSource), "FailureTestCases")]
        public void PreAuthWithError(PaymentModel payment, string responseData, JudoApiError errorType)
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

            IResult<ITransactionResult> paymentReceiptResult = null;

            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            if (payment is CardPaymentModel)
            {
                paymentReceiptResult = judo.Payments.Create((CardPaymentModel)payment).Result;
            }
            else if (payment is TokenPaymentModel)
            {
                paymentReceiptResult = judo.Payments.Create((TokenPaymentModel)payment).Result;
            }
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual((int)errorType, paymentReceiptResult.Error.Code);
        }

        [Test, TestCaseSource(typeof(PreAuthTestSource), "ValidateFailureTestCases")]
        public void ValidatePreAuthsWithoutSuccess(PaymentModel preauth, string responseData, JudoApiError errorType)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient, DotNetLoggerFactory.Create, "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            IResult<ITransactionResult> preAuthReceiptResult = null;

            // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
            if (preauth is CardPaymentModel)
            {
                preAuthReceiptResult = judo.PreAuths.Create((CardPaymentModel)preauth).Result;
            }
            else if (preauth is TokenPaymentModel)
            {
                preAuthReceiptResult = judo.PreAuths.Create((TokenPaymentModel)preauth).Result;
            }
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            Assert.NotNull(preAuthReceiptResult);
            Assert.IsTrue(preAuthReceiptResult.HasError);
            Assert.IsNull(preAuthReceiptResult.Response);
            Assert.IsNotNull(preAuthReceiptResult.Error);
            Assert.AreEqual((int)errorType, preAuthReceiptResult.Error.Code);
        }
    }
}
