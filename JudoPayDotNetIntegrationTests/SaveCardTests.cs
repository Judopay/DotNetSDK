using System;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class SaveCardTest : IntegrationTestsBase
    {
        [Test]
        public void SaveCard()
        {
            var saveCardModel = GetSaveCardModel("432438862");

            var response = JudoPayApiIridium.SaveCards.Create(saveCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void SaveEncryptedCard()
        {
            var saveEncryptedCardModel = GetSaveEncryptedCardModel().Result;

            var response = JudoPayApiIridium.SaveCards.Create(saveEncryptedCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void SaveCardRequiresCv2()
        {
            var saveCardModel = GetSaveCardModel("432438862", "4976000000003436", null);

            var response = JudoPayApiIridium.SaveCards.Create(saveCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasError);
        }

        [Test]
        public void SaveCardAndATokenPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();

            var saveCardModel = GetSaveCardModel(consumerReference);

            var response = JudoPayApiIridium.SaveCards.Create(saveCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;

            var paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 26);

            response = JudoPayApiIridium.Payments.Create(paymentWithToken).Result;

            paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 27);

            response = JudoPayApiIridium.Payments.Create(paymentWithToken).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }
    }
}
