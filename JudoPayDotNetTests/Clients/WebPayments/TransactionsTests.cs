using System;
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

namespace JudoPayDotNetTests.Clients.WebPayments
{
    [TestFixture]
    public class TransactionsTests
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GetByReciptIdRequiresReceiptId(string receiptId)
        {
            var httpClient = Substitute.For<IHttpClient>();
            var client = new Client(new Connection(httpClient, DotNetLoggerFactory.Create, "http://something.com"));
            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            var exception = Assert.Throws<ArgumentNullException>(() => judo.WebPayments.Transactions.GetByReceipt(receiptId));
            Assert.That(exception.Message, Contains.Substring("receiptId"));
        }

        [Test]
        public void GetTransactionsByReceiptId()
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(@"{
    	                                            amount : 10,
    	                                            cardAddress : 
    	                                            {
    		                                            cardHolderName : 'Test User',
    		                                            address1 : 'Test Street',
    		                                            address2 : 'Test Street',
    		                                            address3 : 'Test Street',
    		                                            town : 'London',
    		                                            postCode : 'W31 4HS',
    		                                            country : 'England'
    	                                            },
    	                                            clientIpAddress : '128.0.0.1',
    	                                            clientUserAgent : 'Chrome',
    	                                            companyName : 'Test',
    	                                            currency : 'GBP',
    	                                            expiryDate : '2012-07-19T14:30:00+09:30',
    	                                            judoId : '1254634',
		                                            partnerRecId : '243532',
		                                            paymentCancelUrl : 'http://test.com',
		                                            paymentSuccessUrl : 'http://test.com',
		                                            reference : '42421',
		                                            status : 'Open',
		                                            transactionType : 'SALE',
		                                            yourConsumerReference : '4235325',
		                                            yourPaymentReference : '42355',
		                                            receipt:
		                                            {
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
		                                            }
                                                }") };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            const string RECEIPT_ID = "1245";

            var paymentReceiptResult = judo.WebPayments.Transactions.GetByReceipt(RECEIPT_ID).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.Status, Is.EqualTo(WebPaymentStatus.Open));
            Assert.That(paymentReceiptResult.Response.Receipt.ReceiptId, Is.EqualTo(134567));
        }

        [Test]
        public void GetTransactionsByReference()
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(@"{
    	                                            amount : 10,
    	                                            cardAddress : 
    	                                            {
    		                                            cardHolderName : 'Test User',
    		                                            address1 : 'Test Street',
    		                                            address2 : 'Test Street',
    		                                            address3 : 'Test Street',
    		                                            town : 'London',
    		                                            postCode : 'W31 4HS',
    		                                            country : 'England'
    	                                            },
    	                                            clientIpAddress : '128.0.0.1',
    	                                            clientUserAgent : 'Chrome',
    	                                            companyName : 'Test',
    	                                            currency : 'GBP',
    	                                            expiryDate : '2012-07-19T14:30:00+09:30',
    	                                            judoId : '1254634',
		                                            partnerRecId : '243532',
		                                            paymentCancelUrl : 'http://test.com',
		                                            paymentSuccessUrl : 'http://test.com',
		                                            reference : '42421',
		                                            status : 'Open',
		                                            transactionType : 'SALE',
		                                            yourConsumerReference : '4235325',
		                                            yourPaymentReference : '42355',
		                                            receipt:
		                                            {
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
		                                            }
                                                }") };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            const string REFERENCE = "42421";

            var paymentReceiptResult = judo.WebPayments.Transactions.Get(REFERENCE).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.Reference, Is.EqualTo(REFERENCE));
            Assert.That(paymentReceiptResult.Response.Status, Is.EqualTo(WebPaymentStatus.Open));
            Assert.That(paymentReceiptResult.Response.Receipt.ReceiptId, Is.EqualTo(134567));
        }
    }
}
