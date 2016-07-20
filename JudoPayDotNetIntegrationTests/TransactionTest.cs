using System.Linq;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class TransactionTest : IntegrationTestsBase
    {
        [Test]
        public void GetTransaction()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApi.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var transaction = JudoPayApi.Transactions.Get(response.Response.ReceiptId).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.AreEqual("Success", transaction.Response.Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.ReceiptId);
        }

        [Test]
        public void GetTransactionAfterPreAuthAndTwoCollections()
        {
            var paymentWithCard = GetCardPaymentModel();

            var response = JudoPayApi.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var collection = new CollectionModel()
            {
                Amount = 5,
                ReceiptId = response.Response.ReceiptId
                
            };

            var collectionResult = JudoPayApi.Collections.Create(collection).Result;
            collectionResult = JudoPayApi.Collections.Create(collection).Result;

            var transaction = JudoPayApi.Transactions.Get(response.Response.ReceiptId).Result;

            var payment = transaction.Response as PaymentReceiptModel;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.AreEqual("Success", transaction.Response.Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.ReceiptId);
            Assert.AreEqual(10, payment.AmountCollected);
        }


        [Test]
        public void GetAllTransaction()
        {
            var paymentWithCard = GetCardPaymentModel("66666666");

            var response = JudoPayApi.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            System.Threading.Thread.Sleep(1000);
            var transaction = JudoPayApi.Transactions.Get().Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.IsTrue(transaction.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetPaymentTransactions()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApi.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            System.Threading.Thread.Sleep(1000);
            var transaction = JudoPayApi.Transactions.Get(TransactionType.PAYMENT).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.AreEqual("Success", transaction.Response.Results.First().Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.Results.First().ReceiptId);
        }
    }
}
