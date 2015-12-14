using System;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class RegisterCardTest
    {
        [Test]
        public void RegisterCard()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret, 
                Configuration.Baseaddress);

            var registerCardModel = new CardPaymentModel
            {
                YourConsumerReference = Guid.NewGuid().ToString(),
                CardNumber = "4976000000003436",
                ExpiryDate = "12/15",
                CV2 = "452",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "TR14 8PA",
                    Town = "Town"
                }
            };

            var response = judo.RegisterCards.Create(registerCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void RegisterCardAndATokenPayment()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

            var consumerReference = Guid.NewGuid().ToString();

            var registerCard = new CardPaymentModel
            {
                YourConsumerReference = consumerReference,
                CardNumber = "4976000000003436",
                ExpiryDate = "12/15",
                CV2 = "452",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "TR148PA",
                    Town = "Town"
                }
            };

            var response = judo.RegisterCards.Create(registerCard).Result;

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

            response = judo.Payments.Create(paymentWithToken).Result;

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

            response = judo.Payments.Create(paymentWithToken).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void ADeclinedCardPayment()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);

            var registerCard = new CardPaymentModel
            {
                YourConsumerReference = "432438862",
                CardNumber = "4221690000004963",
                ExpiryDate = "12/15",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = judo.RegisterCards.Create(registerCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }
    }
}
