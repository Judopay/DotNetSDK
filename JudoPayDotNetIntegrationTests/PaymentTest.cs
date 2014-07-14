using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class PaymentTest
    {
        [Test]
        public void ASimplePayment()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET, 
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference    =  "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15"
            };

            var response = judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void ASimpleValidatePayment()
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

            var response = judo.Payments.Validate(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Your good to go!", response.Response.ErrorMessage);
            Assert.AreEqual(20, response.Response.ErrorType);
        }

        [Test]
        public void ATokenPayment()
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

            // Fetch the card token
            var cardToken = response.Response.CardDetails.CardToken;

            var paymentWithToken = new TokenPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardToken = cardToken,
                CV2 = "452"
            };

            response = judo.Payments.Create(paymentWithToken).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void ADeclinedCardPayment()
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
                CardNumber = "4221690000004963",
                CV2 = "125",
                ExpiryDate = "12/15"
            };

            var response = judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }
    }
}
