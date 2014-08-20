using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Clients.Consumer;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients.Consumers
{
    [TestFixture]
    public class ConsumerTransactionsTests
    {
        public class ConsumerTransactionsTestSource
        {

            private const string Consumertoken = "B245SEB";

            public class FunctionHolder
            {
                public Func<IConsumers, IResult<PaymentReceiptResults>> Func;
            }

            public IEnumerable TestData
            {
                get
                {
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("pageSize", "4") },
                        new FunctionHolder
                        {
                            Func = consumers => consumers.GetPayments(Consumertoken, pageSize: 4).Result
                        })
                        .SetName("GetTransactionsJustWithPage");
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("sort", TransactionListSorts.timeAscending.ToString()) },
                        new FunctionHolder
                        {
                            Func = consumers => consumers.GetPayments(Consumertoken, sort: TransactionListSorts.timeAscending).Result
                        })
                        .SetName("GetTransactionsJustWithSort");
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("offset", "0") },
                        new FunctionHolder
                        {
                            Func = consumers => consumers.GetPayments(Consumertoken, offset: 0).Result
                        }).
                        SetName("GetTransactionsJustWithOffset");
                    yield return new TestCaseData(new[]
                            {
                                new KeyValuePair<string, string>("pageSize", "4"),
                                new KeyValuePair<string, string>("offset", "0"),
                                new KeyValuePair<string, string>("sort", TransactionListSorts.timeAscending.ToString())
                            },
                            new FunctionHolder
                            {
                                Func = consumers => consumers.GetPayments(Consumertoken, 4, 0, TransactionListSorts.timeAscending).Result
                            }).SetName("GetTransactionsWithAll");
                }
            }
        }

        [Test, TestCaseSource(typeof(ConsumerTransactionsTestSource), "TestData")]
        public void GetTransactionsForSearchCriterias(KeyValuePair<string, string>[] queryExpected,
                                                      ConsumerTransactionsTestSource.FunctionHolder getCall)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(@"{
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
                            }")};

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://partnerapi.judopay-sandbox.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            getCall.Func(judo.Consumers);

            httpClient.Received().SendAsync(Arg.Any<HttpRequestMessage>());
            var calls = httpClient.ReceivedCalls();

// ReSharper disable once PossibleNullReferenceException
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
            Assert.AreEqual("payments", request.RequestUri.AbsolutePath.Split('/').Last());
        }
    }
}
