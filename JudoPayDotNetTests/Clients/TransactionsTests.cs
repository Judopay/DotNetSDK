using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Clients;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using JudoPayDotNet.Logging;
using NSubstitute;
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

            public static IEnumerable TestData
            {
                get
                {
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("pageSize", "4") },
                        new FunctionHolder
                        {
                           Func = transactions => transactions.Get(TransactionType.PAYMENT, 4).Result
                        })
                        .SetName("GetTransactionsJustWithPage");
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("sort", TransactionListSorts.timeAscending.ToString()) },
                        new FunctionHolder
                        {
                            Func = transactions => transactions.Get(TransactionType.PAYMENT, sort: TransactionListSorts.timeAscending).Result
                        })
                        .SetName("GetTransactionsJustWithSort");
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("offset", "0") },
                        new FunctionHolder
                        {
                            Func = transactions => transactions.Get(TransactionType.PAYMENT, offset: 0).Result
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
                                Func = transactions => transactions.Get(TransactionType.PAYMENT, 4, 0, TransactionListSorts.timeAscending).Result
                            }).SetName("GetTransactionsWithAll");
                }
            }
        }

        [Test]
        public void GetTransactionForReceipt()
        {
            var receiptId = 585759301407084544;
            var acquirerTransactionId = "31746852808191501395";
            var externalBankResponseCode = "12345";
            var postCodeCheckResult = "Passed";
            var cv2CheckResult = "NOT_CHECKED";
            var addressAddress1 = "1 Market House";
            var addressLine2 = "Market Street";
            var town = "MarketTown";
            var postcode = "TR14 8PA";
            var threeDSAttempted = true;
            var threeDSResult = "PASSED";
            var threeDScri = "NoPreference";
            var threeDSeci = "05";
            var numAuthAttempts = 1;

            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent($@"{{
                                receiptId : '{receiptId}',
                                yourPaymentReference: '1d10a1ee-e5a1-4f3b-b7c9-acbb3fc71bee',
                                type : 'Payment',
                                createdAt : '2020-06-04T10:18:20.2767+01:00',
                                result : 'Success',
                                message : 'AuthCode: 229444',
                                judoId : '100915867',
                                merchantName : 'PF Testing',
                                appearsOnStatementAs : 'APL*/PFTesting          ',
                                originalAmount : 1,
                                amount : 1,
                                netAmount : 1,
                                currency : 'GBP',
                                webPaymentReference : '5wcAAAIAAAAUAAAADgAAAOeEZggStJPg_YMvH2PZ_KGy2L09GLdOT2AbTif5rZpSqXbG9w',
                                noOfAuthAttempts : '{numAuthAttempts}',
                                cardDetails :
                                    {{
                                        cardLastfour : '3436',
                                        endDate : '1220',
                                        cardToken : 'XTuS7p7sQ3EZiy1e7oqKPAOCQ1gWOQk9',
                                        cardType : '11',
                                        cardScheme : 'Visa',
                                        cardFunding : 'Debit',
                                        cardCategory : 'Classic',
                                        cardQualifier : 0,
                                        cardCountry : 'FR',
                                        bank : 'Credit Industriel Et Commercial'
                                    }},
                                consumer :
                                    {{
                                        consumerToken : 'vpii5CUSUSt84zpL',
                                        yourConsumerReference : 'cv2 test'
                                    }},
                                device :
                                    {{
                                        identifier : 'd73b4a7b58ce4e54a3bd73b7eda061e6'
                                    }},
                                yourPaymentMetaData :
                                    {{
                                        driver : '338',
                                        bookingId : '97848631'
                                    }},
                                postCodeCheckResult : '{postCodeCheckResult}',
                                acquirerTransactionId : '{acquirerTransactionId}',
                                externalBankResponseCode : '{externalBankResponseCode}',
                                billingAddress :
                                    {{
                                        address1 : '{addressAddress1}',
                                        address2 : '{addressLine2}',
                                        town : '{town}',
                                        postcode : '{postcode}'
                                    }},
                                threeDSecure :
                                    {{
                                        attempted : '{threeDSAttempted}',
                                        result : '{threeDSResult}',
                                        challengeRequestIndicator : '{threeDScri}',
                                        eci : '{threeDSeci}'
                                    }},
                                risks :
                                    {{
                                        postCodeCheck : '{postCodeCheckResult}',
                                        cv2Check : '{cv2CheckResult}'
                                    }}
                             }}") };

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient, 
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var paymentReceiptResult = judo.Transactions.Get(receiptId).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.ReceiptId, Is.EqualTo(receiptId));

            var receipt = (PaymentReceiptModel)paymentReceiptResult.Response;
            //New attributes added for JR-3931
            Assert.That(receipt.AcquirerTransactionId, Is.EqualTo(acquirerTransactionId));
            Assert.That(receipt.ExternalBankResponseCode, Is.EqualTo(externalBankResponseCode));
            Assert.AreEqual(postCodeCheckResult, receipt.Risks.PostcodeCheck);
            Assert.AreEqual(cv2CheckResult, receipt.Risks.Cv2Check);
            Assert.That(receipt.BillingAddress.Address1, Is.EqualTo(addressAddress1));
            Assert.That(receipt.BillingAddress.Address2, Is.EqualTo(addressLine2));
            Assert.That(receipt.BillingAddress.Town, Is.EqualTo(town));
            Assert.That(receipt.BillingAddress.PostCode, Is.EqualTo(postcode));
            Assert.AreEqual(threeDSAttempted, receipt.ThreeDSecure.Attempted);
            Assert.AreEqual(threeDSResult, receipt.ThreeDSecure.Result);
            Assert.AreEqual(threeDScri, receipt.ThreeDSecure.ChallengeRequestIndicator);
            Assert.AreEqual(threeDSeci, receipt.ThreeDSecure.Eci);
            Assert.AreEqual(numAuthAttempts, receipt.NoOfAuthAttempts);
        }

        [Test, TestCaseSource(typeof(TransactionsTestSource),"TestData")]
        public void GetTransactionsForSearchCriterias(KeyValuePair<string, string>[] queryExpected,
                                                      TransactionsTestSource.FunctionHolder getCall)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent((@"{
                            results : [{
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
                             }]}"))};

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient, 
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            getCall.Func(judo.Transactions);

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

            Assert.That(numberOfMatchingParameters, Is.EqualTo(queryExpected.Count()));
            Assert.That(request.RequestUri.AbsolutePath.Split('/').Last(), Is.EqualTo("payments"));
        }
    }
}
