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
                    yield return new TestCaseData(new RefundModel()
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
                    yield return new TestCaseData(new RefundModel()
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
        }


        [Test, TestCaseSource(typeof(RefundsTestSource), "SuccessTestCases")]
        public void RefundWithSuccess(RefundModel refund, string responseData, string receiptId)
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

            IResult<PaymentReceiptModel> paymentReceiptResult = judo.Refunds.Create(refund).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.AreEqual(paymentReceiptResult.Response.ReceiptId, "134567");
        }

        [Test, TestCaseSource(typeof(RefundsTestSource), "FailureTestCases")]
        public void RefundWithError(RefundModel refund, string responseData, long error)
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

            IResult<PaymentReceiptModel> paymentReceiptResult = judo.Refunds.Create(refund).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual(paymentReceiptResult.Error.ErrorType, error);
        }
        
    }
}