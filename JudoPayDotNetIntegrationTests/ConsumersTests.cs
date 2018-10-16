using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class ConsumersTests : IntegrationTestsBase
    {


        public IEnumerable GatewayTestCases
        {
            get { yield return JudoPayApiIridium;}
        }


        [Test]
        // [TestCaseSource(nameof(GatewayTestCases))]
        public void GetTransaction()
        {
            var paymentWithCard = GetCardPaymentModel(Configuration.Iridium_Judoid, "432438862");

            var response = JudoPayApiIridium.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApiIridium.Consumers.GetTransactions(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Iridium_Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetPaymentTransactions()
        {
            var paymentWithCard = GetCardPaymentModel(Configuration.Iridium_Judoid, "432438862");

            var response = JudoPayApiIridium.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApiIridium.Consumers.GetPayments(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Iridium_Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetPreAuthTransactions()
        {
            var paymentWithCard = GetCardPaymentModel(Configuration.Iridium_Judoid, "432438862");

            var response = JudoPayApiIridium.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApiIridium.Consumers.GetPreAuths(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Iridium_Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetCollectionsTransactions()
        {
            var paymentWithCard = GetCardPaymentModel(Configuration.Iridium_Judoid, "432438862");

            var response = JudoPayApiIridium.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var collection = new CollectionModel
            {
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
                
            };

            response = JudoPayApiIridium.Collections.Create(collection).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApiIridium.Consumers.GetCollections(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Iridium_Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }

        [Test]
        public void GetRefundsTransactions()
        {
            var paymentWithCard = GetCardPaymentModel(Configuration.Iridium_Judoid, "432438862");

            var paymentResponse = JudoPayApiIridium.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(paymentResponse);
            Assert.IsFalse(paymentResponse.HasError);
            Assert.AreEqual("Success", paymentResponse.Response.Result);

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = paymentResponse.Response.ReceiptId,
                
            };

            var response = JudoPayApiIridium.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = JudoPayApiIridium.Consumers.GetRefunds(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            // ReSharper disable once PossibleNullReferenceException
            Assert.AreEqual(Configuration.Iridium_Judoid, transactions.Response.Results.FirstOrDefault().JudoId.ToString(CultureInfo.InvariantCulture));
            // ReSharper disable once PossibleNullReferenceException
            Assert.IsTrue(transactions.Response.Results.Any(t => t.ReceiptId == response.Response.ReceiptId));
        }
    }
}