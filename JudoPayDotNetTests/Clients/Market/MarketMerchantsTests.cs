using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Clients.Market;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients.Market
{
    class MarketMerchantsTests
    {
        public class MarketMerchantsTestSource
        {

            public class FunctionHolder
            {
                public Func<IMarketMerchants, IResult<MerchantSearchResults>> Func;
            }

            public static IEnumerable TestData
            {
                get
                {
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("pageSize", "4") },
                        new FunctionHolder
                        {
                            Func = marketMerchants => marketMerchants.Get(pageSize: 4).Result
                        })
                        .SetName("GetTransactionsJustWithPage");
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("sort", TransactionListSorts.timeDescending.ToString()) },
                        new FunctionHolder
                        {
                            Func = marketMerchants => marketMerchants.Get(sort: TransactionListSorts.timeDescending).Result
                        })
                        .SetName("GetTransactionsJustWithSort");
                    yield return new TestCaseData(
                        new[] { new KeyValuePair<string, string>("offset", "0") },
                        new FunctionHolder
                        {
                            Func = marketMerchants => marketMerchants.Get( offset: 0).Result
                        }).
                        SetName("GetTransactionsJustWithOffset");
                    yield return new TestCaseData(new[]
                            {
                                new KeyValuePair<string, string>("pageSize", "4"),
                                new KeyValuePair<string, string>("offset", "0"),
                                new KeyValuePair<string, string>("sort", TransactionListSorts.timeDescending.ToString())
                            },
                            new FunctionHolder
                            {
                                Func = marketMerchants => marketMerchants.Get(4, 0, TransactionListSorts.timeDescending).Result
                            }).SetName("GetTransactionsWithAll");
                }
            }
        }

        [Test, TestCaseSource(typeof(MarketMerchantsTestSource), "TestData")]
        public void GetMerchantsForSearchCriterias(KeyValuePair<string, string>[] queryExpected,
                                                      MarketMerchantsTestSource.FunctionHolder getCall)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(@"{
    	                                            resultCount : 1,
    	                                            pageSize : 1,
    	                                            offest : 0,
    	                                            sort : 'timeAscending',
    	                                            results : 
    	                                            [
    		                                            {
    			                                            partnerReference : 'abs234',
    			                                            merchantLegalName : 'merchant',
    			                                            accessGranted : '2012-07-19T14:30:00+09:30',
    			                                            lastTransaction : '2012-07-19T14:30:00+09:30',
    			                                            scopes : 'test scope',
    			                                            locations : 
    			                                            [
    				                                            {
	    				                                            partnerReference : 'cbd230',
	    				                                            tradingName : 'mertrade',
	    				                                            judoId : '1235465'
	    			                                            }
    			                                            ]
    		                                            }
    	                                            ]
                                                }")};

            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            getCall.Func(judo.Market.Merchants);

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
        }

        [Test, TestCaseSource(typeof(MarketMerchantsTestSource), "TestData")]
        public void GetMerchantsResultsForSearchCriterias(KeyValuePair<string, string>[] queryExpected,
                                                     MarketMerchantsTestSource.FunctionHolder getCall)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(@"{
    	                                            resultCount : 1,
    	                                            pageSize : 1,
    	                                            offest : 0,
    	                                            sort : 'timeAscending',
    	                                            results : 
    	                                            [
    		                                            {
    			                                            partnerReference : 'abs234',
    			                                            merchantLegalName : 'merchant',
    			                                            accessGranted : '2012-07-19T14:30:00+09:30',
    			                                            lastTransaction : '2012-07-19T14:30:00+09:30',
    			                                            scopes : 'test scope',
    			                                            locations : 
    			                                            [
    				                                            {
	    				                                            partnerReference : 'cbd230',
	    				                                            tradingName : 'mertrade',
	    				                                            judoId : '1235465'
	    			                                            }
    			                                            ]
    		                                            }
    	                                            ]
                                                }")};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var results = getCall.Func(judo.Market.Merchants);

            Assert.IsNotNull(results);
            Assert.IsFalse(results.HasError);
            Assert.AreEqual(1, results.Response.ResultCount);
            Assert.AreEqual(1, results.Response.Results.Count());
// ReSharper disable PossibleNullReferenceException
            Assert.AreEqual(1, results.Response.Results.FirstOrDefault().Locations.Count());
            Assert.AreEqual("1235465", results.Response.Results.FirstOrDefault().Locations.FirstOrDefault().JudoId);
// ReSharper restore PossibleNullReferenceException
        }
    }
}
