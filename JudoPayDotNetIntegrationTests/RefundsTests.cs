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
    public class RefundsTests
    {
        [Test]
        public void ASimplePaymentAndRefund()
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
                ExpiryDate = "12/15"
            };

            var response = judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var refund = new RefundModel()
            {
                Amount = 25,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            response = judo.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            Assert.AreEqual("Refund", response.Response.Type);
        }

        [Test]
        public void ARefundValidate()
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
                ExpiryDate = "12/15"
            };

            var response = judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var refund = new RefundModel()
            {
                Amount = 25,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var validateResponse = judo.Refunds.Validate(refund).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsFalse(validateResponse.HasError);
            Assert.AreEqual("Your good to go!", validateResponse.Response.ErrorMessage);
            Assert.AreEqual(JudoApiError.Validation_Passed, validateResponse.Response.ErrorType);
        }

        [Test]
        public void APreAuthTwoCollectionsAndTwoRefunds()
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
                ExpiryDate = "12/15"
            };

            var preAuthResponse = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(preAuthResponse);
            Assert.IsFalse(preAuthResponse.HasError);
            Assert.AreEqual("Success", preAuthResponse.Response.Result);

            var collection = new CollectionModel()
            {
                Amount = 24,
                ReceiptId = int.Parse(preAuthResponse.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var collection1Response = judo.Collections.Create(collection).Result;

            Assert.IsNotNull(collection1Response);
            Assert.IsFalse(collection1Response.HasError);
            Assert.AreEqual("Success", collection1Response.Response.Result);
            Assert.AreEqual("Collection", collection1Response.Response.Type);

            collection = new CollectionModel()
            {
                Amount = 1,
                ReceiptId = int.Parse(preAuthResponse.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var collection2Response = judo.Collections.Create(collection).Result;

            Assert.IsNotNull(collection2Response);
            Assert.IsFalse(collection2Response.HasError);
            Assert.AreEqual("Success", collection2Response.Response.Result);
            Assert.AreEqual("Collection", collection2Response.Response.Type);

            var refund = new RefundModel()
            {
                Amount = 24,
                ReceiptId = int.Parse(collection1Response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var response = judo.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            Assert.AreEqual("Refund", response.Response.Type);

            refund = new RefundModel()
            {
                Amount = 1,
                ReceiptId = int.Parse(collection2Response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            response = judo.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            Assert.AreEqual("Refund", response.Response.Type);
        }
    }
}
