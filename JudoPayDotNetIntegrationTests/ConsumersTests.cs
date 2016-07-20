using System.Globalization;
using System.Linq;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class ConsumersTests : IntegrationTestsBase
    {
        [Test]
        public void GetTransaction()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApi.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApi.Consumers.GetTransactions(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetPaymentTransactions()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApi.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApi.Consumers.GetPayments(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetPreAuthTransactions()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApi.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApi.Consumers.GetPreAuths(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetCollectionsTransactions()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApi.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var collection = new CollectionModel
            {
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
                
            };

            response = JudoPayApi.Collections.Create(collection).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApi.Consumers.GetCollections(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetRefundsTransactions()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var paymentResponse = JudoPayApi.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(paymentResponse);
            Assert.IsFalse(paymentResponse.HasError);
            Assert.AreEqual("Success", paymentResponse.Response.Result);

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = paymentResponse.Response.ReceiptId,
                
            };

            var response = JudoPayApi.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApi.Consumers.GetRefunds(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }
    }
}