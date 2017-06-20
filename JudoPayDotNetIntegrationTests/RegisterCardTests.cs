using System;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class RegisterCardTest : IntegrationTestsBase
    {

        [Test]
        public void RegisterCard()
        {

            var registerCardModel = GetCardPaymentModel("432438862");

            var response = JudoPayApi.RegisterCards.Create(registerCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void RegisterCardAndATokenPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();

            var registerCard = GetCardPaymentModel(consumerReference);

            var response = JudoPayApi.RegisterCards.Create(registerCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;

            var paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 26);

            response = JudoPayApi.Payments.Create(paymentWithToken).Result;

            paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 27);

            response = JudoPayApi.Payments.Create(paymentWithToken).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void ADeclinedCardPayment()
        {
            var registerCard = GetCardPaymentModel("432438862", "4221690000004963", "125");

            var response = JudoPayApi.RegisterCards.Create(registerCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }
    }
}
