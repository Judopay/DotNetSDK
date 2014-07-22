using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class ConsumersTests
    {

        [Test]
        public void GetTransaction()
        {
            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET,
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel()
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

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = judo.Consumers.GetTransactions(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            Assert.AreEqual(Configuration.JUDOID, transactions.Response.Results.FirstOrDefault().JudoId.ToString());
            Assert.AreEqual(response.Response.ReceiptId, transactions.Response.Results.FirstOrDefault().ReceiptId);
        }

        [Test]
        public void GetPaymentTransactions()
        {
            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET,
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel()
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

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = judo.Consumers.GetPayments(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            Assert.AreEqual(Configuration.JUDOID, transactions.Response.Results.FirstOrDefault().JudoId.ToString());
            Assert.AreEqual(response.Response.ReceiptId, transactions.Response.Results.FirstOrDefault().ReceiptId);
        }

        [Test]
        public void GetPreAuthTransactions()
        {
            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET,
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel()
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = judo.Consumers.GetPreAuths(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            Assert.AreEqual(Configuration.JUDOID, transactions.Response.Results.FirstOrDefault().JudoId.ToString());
            Assert.AreEqual(response.Response.ReceiptId, transactions.Response.Results.FirstOrDefault().ReceiptId);
        }

        [Test]
        public void GetCollectionsTransactions()
        {
            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET,
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel()
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var collection = new CollectionModel()
            {
                Amount = 25,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            response = judo.Collections.Create(collection).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = judo.Consumers.GetCollections(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            Assert.AreEqual(Configuration.JUDOID, transactions.Response.Results.FirstOrDefault().JudoId.ToString());
            Assert.AreEqual(response.Response.ReceiptId, transactions.Response.Results.FirstOrDefault().ReceiptId);
        }

        [Test]
        public void GetRefundsTransactions()
        {
            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET,
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel()
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var paymentResponse = judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(paymentResponse);
            Assert.IsFalse(paymentResponse.HasError);
            Assert.AreEqual("Success", paymentResponse.Response.Result);

            var refund = new RefundModel()
            {
                Amount = 25,
                ReceiptId = int.Parse(paymentResponse.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var response = judo.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(paymentReceipt);

            var transactions = judo.Consumers.GetRefunds(paymentReceipt.Consumer.ConsumerToken).Result;

            Assert.IsNotNull(transactions);
            Assert.IsFalse(transactions.HasError);
            Assert.IsNotEmpty(transactions.Response.Results);
            Assert.AreEqual(Configuration.JUDOID, transactions.Response.Results.FirstOrDefault().JudoId.ToString());
            Assert.AreEqual(paymentReceipt.ReceiptId, transactions.Response.Results.FirstOrDefault().ReceiptId);
        }
    }
}
