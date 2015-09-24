using System;
using System.Linq;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class TransactionTest
    {
        [Test]
        public void GetTransaction()
        {
            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

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

            var response = judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var transaction = judo.Transactions.Get(response.Response.ReceiptId).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.AreEqual("Success", transaction.Response.Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.ReceiptId);
        }

        [Test]
        public void GetTransactionAfterPreAuthAndTwoCollections()
        {
            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

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

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var collection = new CollectionModel()
            {
                Amount = 5,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = Guid.NewGuid().ToString()
            };

            var collectionResult = judo.Collections.Create(collection).Result;
            collectionResult = judo.Collections.Create(collection).Result;

            var transaction = judo.Transactions.Get(response.Response.ReceiptId).Result;

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
            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

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

            var response = judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var transaction = judo.Transactions.Get().Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.AreEqual("Success", transaction.Response.Results.First().Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.Results.First().ReceiptId);
        }

        [Test]
        public void GetPaymentTransactions()
        {
            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

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

            var response = judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var transaction = judo.Transactions.Get(TransactionType.PAYMENT).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.AreEqual("Success", transaction.Response.Results.First().Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.Results.First().ReceiptId);
        }
    }
}
