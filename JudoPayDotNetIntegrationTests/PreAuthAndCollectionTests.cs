using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class PreAuthAndCollectionTests : IntegrationTestsBase
    {
        [Test]
        public void ASimplePreAuth()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApi.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("PreAuth", receipt.Type);
        }

        [Test]
        public void ADeclinedCardPreAuth()
        {
            var paymentWithCard = GetCardPaymentModel("432438862", "4221690000004963", "125");

            var response = JudoPayApi.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }

        [Test]
        public void ASimplePreAuthAndCollection()
        {
            var paymentWithCard = GetCardPaymentModel();

            var response = JudoPayApi.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("PreAuth", receipt.Type);

            var collection = new CollectionModel
            {
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
                
            };

            response = JudoPayApi.Collections.Create(collection).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);
        }

        [Test]
        public void AFailedSimplePreAuthAndValidateCollection()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response = JudoPayApi.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("PreAuth", receipt.Type);

            var collection = new CollectionModel
            {
                Amount = 20,
                ReceiptId = response.Response.ReceiptId,
            };

            var collectionResponse = JudoPayApi.Collections.Create(collection).Result;

            // The collection will go through since it is less than the preauth amount
            Assert.That(collectionResponse.HasError, Is.False);
            Assert.That(collectionResponse.Response.ReceiptId, Is.GreaterThan(0));

            var validateResponse = JudoPayApi.Collections.Create(collection).Result;

            Assert.That(validateResponse, Is.Not.Null);
            Assert.That(validateResponse.HasError, Is.True);
            Assert.That(validateResponse.Error.Message, Is.EqualTo("Sorry, but the amount you're trying to collect is greater than the pre-auth"));
            Assert.That(validateResponse.Error.Code, Is.EqualTo(46));
        }
    }
}