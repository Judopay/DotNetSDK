using System;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class PaymentTest
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
        public void ASimplePayment()
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

            var result = _judo.Payments.Create(paymentWithCard);
            var response = result.Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void ASimpleValidatePayment()
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

            var response = _judo.Payments.Validate(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Your good to go!", response.Response.ErrorMessage);
            Assert.AreEqual(JudoApiError.Validation_Passed, response.Response.ErrorType);
        }

        [Test]
        public void ATokenPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();

            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = consumerReference,
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "TR148PA",
                    Town = "Town"
                }
            };

            var response = _judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;

            var paymentWithToken = new TokenPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = consumerReference,
                Amount = 26,
                CardToken = cardToken,
                CV2 = "452",
                ConsumerToken = "ABSE"
            };

            response = _judo.Payments.Create(paymentWithToken).Result;

            paymentWithToken = new TokenPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = consumerReference,
                Amount = 27,
                CardToken = cardToken,
                CV2 = "452",
                ConsumerToken = "ABSE"
            };

            response = _judo.Payments.Create(paymentWithToken).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void ADeclinedCardPayment()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4221690000004963",
                CV2 = "125",
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
            Assert.AreEqual("Declined", response.Response.Result);
        }
    }
}