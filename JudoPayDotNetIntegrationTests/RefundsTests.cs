using System;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class RefundsTests
    {
        private JudoPayApi _judo;

        [TestFixtureSetUp]
        public void SetupOnce()
        {
            _judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);
        }

        [Test]
        public void ASimplePaymentAndRefund()
        {
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

            var response = _judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            response = _judo.Refunds.Create(refund).Result;

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

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var validateResponse = _judo.Refunds.Validate(refund).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsFalse(validateResponse.HasError);
            Assert.AreEqual("Your good to go!", validateResponse.Response.ErrorMessage);
            Assert.AreEqual(JudoApiError.Validation_Passed, validateResponse.Response.ErrorType);
        }

        [Test]
        public void APreAuthTwoCollectionsAndTwoRefunds()
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

            var preAuthResponse = _judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(preAuthResponse);
            Assert.IsFalse(preAuthResponse.HasError);
            Assert.AreEqual("Success", preAuthResponse.Response.Result);

            var collection = new CollectionModel
            {
                Amount = 24,
                ReceiptId = int.Parse(preAuthResponse.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var collection1Response = _judo.Collections.Create(collection).Result;

            Assert.IsNotNull(collection1Response);
            Assert.IsFalse(collection1Response.HasError);

            var receipt = collection1Response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);

            collection = new CollectionModel
            {
                Amount = 1,
                ReceiptId = int.Parse(preAuthResponse.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var collection2Response = _judo.Collections.Create(collection).Result;

            Assert.IsNotNull(collection2Response);
            Assert.IsFalse(collection2Response.HasError);

            receipt = collection2Response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);

            var refund = new RefundModel
            {
                Amount = 24,
                ReceiptId = int.Parse(collection1Response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var response = _judo.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);

            refund = new RefundModel
            {
                Amount = 1,
                ReceiptId = int.Parse(collection2Response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            response = _judo.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);
        }
    }
}
