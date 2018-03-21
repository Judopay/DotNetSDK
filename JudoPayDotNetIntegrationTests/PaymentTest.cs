using System;
using System.Threading.Tasks;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class PaymentTest : IntegrationTestsBase
    {
        [Test]
        public void ASimplePayment()
        {
            var paymentWithCard = GetCardPaymentModel();

            var result = JudoPayApi.Payments.Create(paymentWithCard);
            var response = result.Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        [Ignore("Till recurring flag is exposed on partnerapi")]
        public void ARecurringPayment()
        {
            var paymentWithCard = GetCardPaymentModel(recurringPayment: true, judoId: Configuration.Cybersource_Judoid);

            var judoPay = UseCybersourceGateway();

            var result = judoPay.Payments.Create(paymentWithCard);
            var response = result.Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            var paymentReceipt = response.Response as PaymentReceiptModel;
            Assert.IsInstanceOf<PaymentReceiptModel>(response.Response);
            Assert.IsTrue(paymentReceipt.Recurring);
        }

        [Test]
        public void ATokenPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();

            var paymentWithCard = GetCardPaymentModel(consumerReference);

            var response = JudoPayApi.Payments.Create(paymentWithCard).Result;

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
        [Ignore("Till recurring flag is exposed on partnerapi")]
        public void ATokenRecurringPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();

            var paymentWithCard = GetCardPaymentModel(consumerReference, recurringPayment: true, judoId: Configuration.Cybersource_Judoid);

            var judoPay = UseCybersourceGateway();

            var response = judoPay.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            var paymentReceipt = response.Response as PaymentReceiptModel;
            Assert.IsInstanceOf<PaymentReceiptModel>(response.Response);
            Assert.IsTrue(paymentReceipt.Recurring);
        }

        [Test]
        public void AOneTimePayment()
        {
            var oneTimePaymentModel = GetOneTimePaymentModel().Result;

            var response = JudoPayApi.Payments.Create(oneTimePaymentModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);
        }

        [Test]
        public void ADeclinedCardPayment()
        {
            var paymentWithCard = GetCardPaymentModel("432438862", "4221690000004963", "125");

            var response = JudoPayApi.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }

        [Test]
        public void DeDuplicationTest()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response1 = JudoPayApi.Payments.Create(paymentWithCard).Result;
            
            var response2 = JudoPayApi.Payments.Create(paymentWithCard).Result;
          
            Assert.AreEqual(response1.Response.ReceiptId, response2.Response.ReceiptId);
        }

        [Test]
        public void VisaCheckoutPayment()
        {
            var callId = "2535293917395986901";
            var encKey = "DL1znS1YD0D4L518up18ibGQK25jPiklD0k58PQQc775n/AOeBrXD292I1ywmXVjW10ngTGhuCW4ifEBfr1vxEe4vXHg7oXDY7+pO1tvNDV3G4Fkmy0Q3H8ieZ1RfD2y";
            var encPaymentData = "EuWD12Sem0WTNRbIsCG6ATsABxssMvbPvFbR8SqK1Jj7YeU6a7s9Bs2Gk7I94E9p6ghEvl/wePm9nhamebj7vBzdLj/ezOqBgl41JUNq+uhRfS5zB9+7nAZkldwpT9TaLECbb+4JY1W9osUoZXL7XtnKz+OTeBG3apnjDomVf9kaNpbu0RkQFzclTyoeeODTDtpoZ+lqumR1E7yr/LsG5JtM+GvAdoULSgX3Xc4IaCRCWbGwKZaTm8PadXuyPiZXpcRCZd0I0KBDj8RGHW7wl3rPfpx4SxB4YMBieJNFbdj/AU2hWnU8fYEtgPaWKaA31a4csHFvQSBsmGAuJLEQ7CKb63k1EZV7rMJR/If/jNz17gtVHWjaEb/SNHybHe7MzDkmjhODfFR+ceaHb9b/NwD1X9QGBXSwrXwMZR06vdyao//T44NbhhfPngGORSz47sHx2o40ECIfo5HGB/BbZO48Sl61pNKommEjEj0JabbzOqNEdA7IJkf1I1eO0bPTW41Vd84Fgofe2aKy2987lPhMnJdHFaslo6DS8cclF7LySQWYjOZNSL+0tW9hLLV1DO1H0xvYA7CsvHyutyDxTVtiOkl5+MKeAzRsaWQTC4sbxrgWZjYbxGCeWOWED+xQRSeJmY1J4hpUjB7jwGymekBXVfiHvwM9sAgZ2+EU95j6kRYciKhLOMgYH68wRJqHR4wR4rjxiHrjwZARqnG/5vwYZ9qvJxyKUkE+lcMNLK8x18tT4N1nm2oPW08GnbkRXmr2SFnv3RwuxbIOyaKIJrNjeQ0ybG4+taQcdRyllNpKwY04sxN3seC5w4/85x302oVzpaJAM22SIqawTn20kKIQylJI3X/w+LoPHthrGdT56mLAntjIB0P4Wnz1l2APaXfgtvKyX5HoM7acgTucEFKfiWEeBMcamu9XqSBQWELTXQls6CDTNrcAM2X4oEJSUaacTarCVWUmrluuNr+AO3YF9NMil35ES+P6+/Z0ikWqi5OFxNTy3milrsyjwEakmRS7J3/hMhAkcVmG7vUeBqSYmqa+p3KcL+0R1pjv//eh6Zw5gj7LHmgEmsuNmdUyW/kw19ekCU2ToLUpFtS64PUUB0lvFXP/abX2zEjzEYmYwhtr1Bgt3AAlc+z4LucUFm9m2jYWW+dbv5ivHHlGcLD8qJi455rQGzu316mFYUeAQ6nQmza6MYWQ2UvaCm38f5F7VOzMWrW447whNtKpXAb8DUDGUiXglwChXeEyc7UEmYDuy2XD1iEt3x8s38pOGjePXVu5ItyD2IV4jD/U4DwzYvByRzErQLZglLeWoqoeGsINXecu4PIoNgfnt8VLVjVwsI54IuNkkjk9KPQ/BqYQoPV2SzAhGDV69BK9KntnlKhPsdLcPNLEXLHn9nYLobFpFj4Gha1oxSQ4KwU8oNYvypXWZ17cp2LPbbYeRpjygHGwIVait8x8ztF9EXDBFC28V6WpQKjlKpKcAQooZs3JjYPXqhaUrMEgpBCRthtB434SXQo3v82CltQYOXQiOgvrWWjf67vVSOojqJRzqzNqC2v7HNvAzjKXccCfO1SzomzHKan86hvDJBmjPR7UhV/qtNuIFL9bh/WBHZEKRDdNuGQMzAl9vO7V096tuCCtOlqjy4ICXY/eC7M3mtydSFyVrKShcUHwmBOaB2bnQcXd8sQaT9kcihjmRWd+P8t2O53vAavMmUSdGu0Hb+BS5uw/yW41ne3kUbhm6L6bFy/BiNEwNsKLyen4K1lvGsTgwFUD64lpPurWWzFTXhffqSKPIPqWxhoDMBWU46DmxsBXevzUNsq2mR5zWcB1kGpuR3uJHEZ4zc+CThqAPsKchFC+uGahCE+jldbcvGQE3RCzdzZRmyjxe4jttuExhLE=";
            var paymentWithCard = GetVisaCheckoutPaymentModel(callId, encKey, encPaymentData);

            var result = JudoPayApi.Payments.Create(paymentWithCard);
            var response = result.Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }
    }
}