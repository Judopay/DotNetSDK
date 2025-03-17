using System;
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

            var response = JudoPayApiBase.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var transaction = JudoPayApiBase.Transactions.Get(response.Response.ReceiptId).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.AreEqual("Success", transaction.Response.Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.ReceiptId);

            var receipt = transaction.Response as PaymentReceiptModel;
            Assert.IsNotNull(receipt);
            Assert.IsNotNull(receipt.Acquirer);
            Assert.That(receipt.AuthCode, Does.Match("\\d{6}"), $"AuthCode on receipt not in correct format xxxxxx. Was {receipt.AuthCode}");
        }

        [Test]
        // For JR-4723
        public void GetTransactionFromWebPayment()
        {
            // Given a WebPayment session
            var yourConsumerReference = "432438862";
            var webPaymentRequest = new WebPaymentRequestModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                Amount = 25
            };

            var webPaymentResult = JudoPayApiBase.WebPayments.Payments.Create(webPaymentRequest).Result;
            Assert.NotNull(webPaymentResult);
            Assert.IsFalse(webPaymentResult.HasError);

            var webPaymentReference = webPaymentResult.Response.Reference;
            Assert.NotNull(webPaymentReference);

            // And an associated payment (passing webPaymentReference)
            var paymentWithCard = GetCardPaymentModel();
            paymentWithCard.WebPaymentReference = webPaymentReference;

            // Set other fields to be identical for the authentication to be successful 
            paymentWithCard.Amount = webPaymentRequest.Amount;
            paymentWithCard.YourConsumerReference = webPaymentRequest.YourConsumerReference;
            paymentWithCard.YourPaymentReference = webPaymentRequest.YourPaymentReference;

            var response = JudoPayApiBase.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            // When receiptId of payment is retrieved
            var transaction = JudoPayApiBase.Transactions.Get(response.Response.ReceiptId).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.AreEqual("Success", transaction.Response.Result);
            Assert.AreEqual(response.Response.ReceiptId, transaction.Response.ReceiptId);

            var receipt = transaction.Response as PaymentReceiptModel;
            Assert.IsNotNull(receipt);

            // Then original WebPaymentReference is shown
            Assert.AreEqual(webPaymentReference, receipt.WebPaymentReference);
            // And AuthCode and Acquirer are also set
            Assert.That(receipt.AuthCode, Does.Match("\\d{6}"), $"AuthCode on receipt not in correct format xxxxxx. Was {receipt.AuthCode}");
            Assert.IsNotNull(receipt.Acquirer);

        }

        [Test]
        public void GetTransactionAfterPreAuthAndTwoCollections()
        {
            var paymentWithCard = GetCardPaymentModel();

            var response = JudoPayApiBase.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var collection = new CollectionModel()
            {
                Amount = 5,
                ReceiptId = response.Response.ReceiptId
            };
            var collection2 = new CollectionModel()
            {
                Amount = 5,
                ReceiptId = response.Response.ReceiptId
            };
            var collectionResult = JudoPayApiBase.Collections.Create(collection).Result;
            collectionResult = JudoPayApiBase.Collections.Create(collection2).Result;

            var transaction = JudoPayApiBase.Transactions.Get(response.Response.ReceiptId).Result;

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

            var response = JudoPayApiBase.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            System.Threading.Thread.Sleep(10000);
            var transaction = JudoPayApiBase.Transactions.Get().Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.IsTrue(transaction.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetPaymentTransactions()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApiBase.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            System.Threading.Thread.Sleep(10000);
            var transaction = JudoPayApiBase.Transactions.Get(TransactionType.PAYMENT).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.AreEqual("Success", transaction.Response.Results.First().Result);
            Assert.IsTrue(transaction.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetTransactionWithPaymentReference()
        {
            var paymentWithCard = GetCardPaymentModel();
            var merchantPaymentRef = Guid.NewGuid().ToString();
            paymentWithCard.YourPaymentReference = merchantPaymentRef;

            var response = JudoPayApiBase.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            System.Threading.Thread.Sleep(10000);
            var transaction = JudoPayApiBase.Transactions.Get(yourPaymentReference: merchantPaymentRef).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.AreEqual(1, transaction.Response.Results.Count());
            Assert.AreEqual(merchantPaymentRef, transaction.Response.Results.First().YourPaymentReference);
        }

        [Test]
        public void GetTransactionWithConsumerReference()
        {
            var merchantConsumerRef = Guid.NewGuid().ToString();
            var paymentWithCard = GetCardPaymentModel(merchantConsumerRef);

            var response = JudoPayApiBase.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            System.Threading.Thread.Sleep(10000);
            var transaction = JudoPayApiBase.Transactions.Get(yourConsumerReference: merchantConsumerRef).Result;

            Assert.IsNotNull(transaction);
            Assert.IsFalse(transaction.HasError);
            Assert.IsNotEmpty(transaction.Response.Results);
            Assert.AreEqual(1, transaction.Response.Results.Count());
            Assert.AreEqual(merchantConsumerRef, transaction.Response.Results.First().Consumer.YourConsumerReference);
        }

        [Test]
        public void GetTransactionWithFromDateInFuture()
        {
            var fromString = DateTimeOffset.Now.AddDays(1).ToString("dd/MM/yyyy");

            System.Threading.Thread.Sleep(1000);
            var transactionList = JudoPayApiBase.Transactions.Get(from: fromString).Result;

            Assert.IsNotNull(transactionList);
            Assert.IsFalse(transactionList.HasError);
            Assert.IsEmpty(transactionList.Response.Results);
        }
    }
}
