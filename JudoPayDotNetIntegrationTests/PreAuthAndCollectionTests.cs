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
        public void ADeclinedValidationOnCardPreAuth()
        {
            var paymentWithCard = GetCardPaymentModel("432438862", "4221690000004963", "125");

            var response = JudoPayApi.PreAuths.Validate(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual(JudoApiError.General_Error, response.Response.ErrorType);
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
        public void ASimplePreAuthAndValidateCollection()
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
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
                
            };

            var validateResponse = JudoPayApi.Collections.Validate(collection).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsFalse(validateResponse.HasError);
            Assert.AreEqual(JudoApiError.General_Error, validateResponse.Response.ErrorType);
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
                Amount = 1,
                ReceiptId = response.Response.ReceiptId,
                
            };

            var test = JudoPayApi.Collections.Create(collection).Result;
            collection.Amount = 30;
            var validateResponse = JudoPayApi.Collections.Validate(collection).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsTrue(validateResponse.HasError);
            Assert.True(string.Equals("Sorry, but the amount you're trying to collect is greater than the pre-auth", validateResponse.Error.Message));
            Assert.True(46==validateResponse.Error.Code);
        }


    }
}