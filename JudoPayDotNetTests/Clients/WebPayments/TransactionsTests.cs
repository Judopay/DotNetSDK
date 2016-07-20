using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients.WebPayments
{
    [TestFixture]
    public class TransactionsTests
    {
        [Test]
        public void GetTransactionsByReceiptId()
        {
            var httpClient = Substitute.For<IHttpClient>();
            var response = new HttpResponseMessage(HttpStatusCode.OK) {Content = new StringContent(@"{
    	                                            amount : 10,
    	                                            cardAddress : 
    	                                            {
    		                                            cardHolderName : 'Test User',
    		                                            line1 : 'Test Street',
    		                                            line2 : 'Test Street',
    		                                            line3 : 'Test Street',
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
		                                            partnerServiceFee : 10,
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
	                                                            consumerToken : 'B245SEB',
	                                                            yourConsumerReference : 'Consumer1'
	                                                        }
		                                            }
                                                }")};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            const string receiptId = "1245";

            var paymentReceiptResult = judo.WebPayments.Transactions.GetByReceipt(receiptId).Result;

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
    		                                            line1 : 'Test Street',
    		                                            line2 : 'Test Street',
    		                                            line3 : 'Test Street',
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
		                                            partnerServiceFee : 10,
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
	                                                            consumerToken : 'B245SEB',
	                                                            yourConsumerReference : 'Consumer1'
	                                                        }
		                                            }
                                                }")};
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var responseTask = new TaskCompletionSource<HttpResponseMessage>();
            responseTask.SetResult(response);

            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(responseTask.Task);

            var client = new Client(new Connection(httpClient,
                                                    DotNetLoggerFactory.Create,
                                                    "http://something.com"));

            var judo = new JudoPayApi(DotNetLoggerFactory.Create, client);

            const string reference = "42421";

            var paymentReceiptResult = judo.WebPayments.Transactions.Get(reference).Result;

            Assert.NotNull(paymentReceiptResult);
            Assert.IsFalse(paymentReceiptResult.HasError);
            Assert.NotNull(paymentReceiptResult.Response);
            Assert.That(paymentReceiptResult.Response.Reference, Is.EqualTo(reference));
            Assert.That(paymentReceiptResult.Response.Status, Is.EqualTo(WebPaymentStatus.Open));
            Assert.That(paymentReceiptResult.Response.Receipt.ReceiptId, Is.EqualTo(134567));
        }
    }
}
