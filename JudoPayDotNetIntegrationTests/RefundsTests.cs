using System;
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
            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
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

            var response = judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
                YourPaymentReference = "578543"
            };

            response = judo.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);
        }

        [Test]
        public void ARefundValidate()
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

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
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

            var preAuthResponse = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(preAuthResponse);
            Assert.IsFalse(preAuthResponse.HasError);
            Assert.AreEqual("Success", preAuthResponse.Response.Result);

            var collection = new CollectionModel
            {
                Amount = 24,
                ReceiptId = preAuthResponse.Response.ReceiptId,
                YourPaymentReference = "578543"
            };

            var collection1Response = judo.Collections.Create(collection).Result;

            Assert.IsNotNull(collection1Response);
            Assert.IsFalse(collection1Response.HasError);

            var receipt = collection1Response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);

            collection = new CollectionModel
            {
                Amount = 1,
                ReceiptId = preAuthResponse.Response.ReceiptId,
                YourPaymentReference = "578543"
            };

            var collection2Response = judo.Collections.Create(collection).Result;

            Assert.IsNotNull(collection2Response);
            Assert.IsFalse(collection2Response.HasError);

            receipt = collection2Response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);

            var refund = new RefundModel
            {
                Amount = 24,
                ReceiptId = collection1Response.Response.ReceiptId,
                YourPaymentReference = "578543"
            };

            var response = judo.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);

            refund = new RefundModel
            {
                Amount = 1,
                ReceiptId = collection2Response.Response.ReceiptId,
                YourPaymentReference = "578543"
            };

            response = judo.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);
        }
    }
}
