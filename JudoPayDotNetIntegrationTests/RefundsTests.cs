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
            var paymentWithCard = GetCardPaymentModel(Configuration.Iridium_Judoid);
            var response = JudoPayApiIridium.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
               
            };

            response = JudoPayApiIridium.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);
        }

        [Test]
        public void APreAuthTwoCollectionsAndTwoRefunds()
        {
            var paymentWithCard = GetCardPaymentModel(
                judoId: Configuration.Iridium_Judoid,
                yourConsumerReference: "432438862"
            );

            var preAuthResponse = JudoPayApiIridium.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(preAuthResponse);
            Assert.IsFalse(preAuthResponse.HasError);
            Assert.AreEqual("Success", preAuthResponse.Response.Result);

            var collection = new CollectionModel
            {
                Amount = 24,
                ReceiptId = preAuthResponse.Response.ReceiptId,
                
            };

            var collection1Response = JudoPayApiIridium.Collections.Create(collection).Result;

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

            var collection2Response = JudoPayApiIridium.Collections.Create(collection).Result;

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

            var response = JudoPayApiIridium.Refunds.Create(refund).Result;

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

            response = JudoPayApiIridium.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);
        }
    }
}
