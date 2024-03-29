﻿using System.Collections;
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
    public class VoidsTests
    {
        //Test data
        private class VoidsTestSource
        {
            public static IEnumerable CreateSuccessTestCases
            {
                get
                {
                    yield return new TestCaseData(new VoidModel { Amount = 2.0m, ReceiptId = "34560", }, @"{
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
                                    yourConsumerReference : 'Consumer1'
                                }
                            }", "134567").SetName("VoidWithSuccess");
                }
            }

            public static IEnumerable CreateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new VoidModel { Amount = 2.0m, ReceiptId = "34560", }, @"
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
                        }", JudoApiError.Payment_Declined).SetName("VoidWithoutSuccess");
                }
            }
        }

        [Test, TestCaseSource(typeof(VoidsTestSource), nameof(VoidsTestSource.CreateSuccessTestCases))]
        public void VoidWithSuccess(VoidModel voids, string responseData, string receiptId)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient, DotNetLoggerFactory.Create, "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.Voids.Create(voids).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.ReceiptId, Is.EqualTo("134567"));
        }


        [Test, TestCaseSource(typeof(VoidsTestSource), nameof(VoidsTestSource.CreateSuccessTestCases))]
        public void ExtraHeadersAreSent(VoidModel voids, string responseData, string receiptId)
        {
            const string EXTRA_HEADER_NAME = "X-Extra-Request-Header";

            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Is<HttpRequestMessage>(r => r.Headers.Contains(EXTRA_HEADER_NAME))).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient, DotNetLoggerFactory.Create, "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            voids.HttpHeaders.Add(EXTRA_HEADER_NAME, "some random value");

            IResult<ITransactionResult> refundReceipt = judo.Voids.Create(voids).Result;

            Assert.NotNull(refundReceipt);
            Assert.IsFalse(refundReceipt.HasError);
            Assert.NotNull(refundReceipt.Response);
            Assert.That(refundReceipt.Response.ReceiptId, Is.EqualTo("134567"));
        }

        [Test, TestCaseSource(typeof(VoidsTestSource), nameof(VoidsTestSource.CreateFailureTestCases))]
        public void CollectionWithError(VoidModel voids, string responseData, JudoApiError error)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(responseData) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient, DotNetLoggerFactory.Create, "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.Voids.Create(voids).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual((int)error, paymentReceiptResult.Error.Code);
        }
    }
}
