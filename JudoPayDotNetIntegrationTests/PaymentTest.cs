using System;
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
    }
}