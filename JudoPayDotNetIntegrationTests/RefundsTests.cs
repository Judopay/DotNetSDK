using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class RefundsTests : IntegrationTestsBase
    {
        [Test]
        public void ASimplePaymentAndRefund()
        {
            var paymentWithCard = GetCardPaymentModel();
            var response = JudoPayApi.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
               
            };

            response = JudoPayApi.Refunds.Create(refund).Result;

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
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApi.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
                
            };

            var validateResponse = JudoPayApi.Refunds.Validate(refund).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsFalse(validateResponse.HasError);
            Assert.AreEqual(JudoApiError.General_Error, validateResponse.Response.ErrorType);
        }

        [Test]
        public void APreAuthTwoCollectionsAndTwoRefunds()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var preAuthResponse = JudoPayApi.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(preAuthResponse);
            Assert.IsFalse(preAuthResponse.HasError);
            Assert.AreEqual("Success", preAuthResponse.Response.Result);

            var collection = new CollectionModel
            {
                Amount = 24,
                ReceiptId = preAuthResponse.Response.ReceiptId,
                
            };

            var collection1Response = JudoPayApi.Collections.Create(collection).Result;

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
                
            };

            var collection2Response = JudoPayApi.Collections.Create(collection).Result;

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
                
            };

            var response = JudoPayApi.Refunds.Create(refund).Result;

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
                
            };

            response = JudoPayApi.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);
        }
    }
}
