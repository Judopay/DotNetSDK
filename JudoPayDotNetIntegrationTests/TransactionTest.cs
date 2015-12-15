using System;
using System.Linq;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class TransactionTest
    {
        private JudoPayApi _judo;

        [TestFixtureSetUp]
        public void Init()
        {
            _judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);
        }

        [Test]
        public void GetTransaction()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = _judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var transaction = _judo.Transactions.Get(response.Response.ReceiptId).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.AreEqual("Success", transaction.Response.Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.ReceiptId);
        }

        [Test]
        public void GetTransactionAfterPreAuthAndTwoCollections()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = Guid.NewGuid().ToString(),
                YourConsumerReference = Guid.NewGuid().ToString(),
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "TR14 8PA",
                    Town = "Town"
                }
            };

            var response = _judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var collection = new CollectionModel()
            {
                Amount = 5,
                ReceiptId = response.Response.ReceiptId,
                YourPaymentReference = Guid.NewGuid().ToString()
            };

            var collectionResult = _judo.Collections.Create(collection).Result;
            collectionResult = _judo.Collections.Create(collection).Result;

            var transaction = _judo.Transactions.Get(response.Response.ReceiptId).Result;

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
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "66666666",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = _judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            System.Threading.Thread.Sleep(1000);
            var transaction = _judo.Transactions.Get().Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.IsTrue(transaction.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
           // Assert.AreEqual(response.Response.ReceiptId, transaction.Response.Results.First().ReceiptId);
        }

        [Test]
        public void GetPaymentTransactions()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = _judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            System.Threading.Thread.Sleep(1000);
            var transaction = _judo.Transactions.Get(TransactionType.PAYMENT).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.AreEqual("Success", transaction.Response.Results.First().Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.Results.First().ReceiptId);
        }
    }
}
