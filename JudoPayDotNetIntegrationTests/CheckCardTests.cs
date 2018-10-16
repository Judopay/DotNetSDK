using System;
using System.Threading.Tasks;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class CheckCardTests : IntegrationTestsBase
    {
        [Test]
        public async Task CheckCard()
        {
            var checkCardModel = GetCheckCardModel(Configuration.Cybersource_Judoid);

            var response = await JudoPayApiCyberSource.CheckCards.Create(checkCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var receipt = response.Response as PaymentReceiptModel;
            Assert.NotNull(receipt);
            Assert.AreEqual("CheckCard", receipt.Type);
        }

        [Test]
        public async Task CheckCardAndATokenPayment()
        {
            var checkCardModel = GetCheckCardModel(Configuration.Cybersource_Judoid);

            var response = await JudoPayApiCyberSource.CheckCards.Create(checkCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;
            var consumerReference = receipt.Consumer.YourConsumerReference;

            var paymentWithToken = GetTokenPaymentModel(
                cardToken,
                Configuration.Cybersource_Judoid,
                consumerReference, 
                27
            );

            response = await JudoPayApiCyberSource.Payments.Create(paymentWithToken);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }
    }
}
