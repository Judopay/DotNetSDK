using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Client;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet.Logging;
using JudoPayDotNetTests.Clients;
using NSubstitute;
using NSubstitute.Routing.Handlers;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients
{
    public class TransactionsTests
    {

        public class TransactionsTestSource 
        {

            public class FunctionHolder
            {
                public Func<ITransactions, IResult<PaymentReceiptResults>> Func;
            }

            public IEnumerable TestData
            {
                get
                {
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("pageSize", "4") },
                        new FunctionHolder(){
                           Func = transactions => transactions.Get("SALE", pageSize: 4).Result
                        })
                        .SetName("GetTransactionsJustWithPage");
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("sort", "ASC") },
                        new FunctionHolder(){
                            Func = transactions => transactions.Get("SALE", sort: "ASC").Result
                        })
                        .SetName("GetTransactionsJustWithSort");
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("offset", "0") },
                        new FunctionHolder(){
                            Func = transactions => transactions.Get("SALE", offset: 0).Result
                        }).
                        SetName("GetTransactionsJustWithOffset");
                    yield return new TestCaseData(new[]
                            {
                                new KeyValuePair<string, string>("pageSize", "4"),
                                new KeyValuePair<string, string>("offset", "0"),
                                new KeyValuePair<string, string>("sort", "ASC"),
                            },
                            new FunctionHolder(){
                                Func = transactions => transactions.Get("SALE", 4, 0, "ASC").Result
                            }).SetName("GetTransactionsWithAll");
                }
            }
        }

        [Test]
        public void GetTransationsForReceipt()
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(@"{
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
                            }");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var credentials = new Credentials("ABC", "Secrete");
            var client = new Client(new Connection(httpClient, 
                                                    DotNetLoggerFactory.Create(typeof(Connection)), 
                                                    "http://judo.com"));

            JudoPayments judo = new JudoPayments(credentials, client);

            var receiptId = "1245";

            IResult<PaymentReceiptModel> paymentReceiptResult = judo.Transactions.Get(receiptId).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.AreEqual(paymentReceiptResult.Response.ReceiptId, "134567");
        }

        [Test, TestCaseSource(typeof(TransactionsTestSource),"TestData")]
        public void GetTransactionsForSearchCriterias(KeyValuePair<string, string>[] queryExpected,
                                                      TransactionsTestSource.FunctionHolder getCall)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(@"{
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
                            }");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var credentials = new Credentials("ABC", "Secrete");
            var client = new Client(new Connection(httpClient, 
                                                    DotNetLoggerFactory.Create(typeof(Connection)), 
                                                    "http://judo.com"));

            JudoPayments judo = new JudoPayments(credentials, client);

            getCall.Func(judo.Transactions);

            httpClient.Received().SendAsync(Arg.Any<HttpRequestMessage>());
            var calls = httpClient.ReceivedCalls();

            var request = calls.FirstOrDefault(call => call.GetMethodInfo().Name == "SendAsync").
                                    GetArguments().FirstOrDefault() as HttpRequestMessage;

            Assert.IsNotNull(request);

            var numberOfMatchingParameters = request.RequestUri.Query
                                    .Remove(0, 1)
                                    .Split('&').Select(kv =>
                                    {
                                        var keyValue = kv.Split('=');
                                        return new KeyValuePair<string, string>(keyValue[0], keyValue[1]);
                                    }).Intersect(queryExpected).Count();

            Assert.AreEqual(queryExpected.Count(), numberOfMatchingParameters);
            Assert.AreEqual("SALE", request.RequestUri.AbsolutePath.Split('/').Last());
        }
    }
}
