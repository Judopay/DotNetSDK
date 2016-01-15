using System;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class PreAuthAndCollectionTests
    {
        private JudoPayApi _judo;

        [OneTimeSetUp]
        public void Init()
        {
            _judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);
        }

 
        [Test]
        public void ASimplePreAuth()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = _judo.PreAuths.Create(paymentWithCard).Result;

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
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4221690000004963",
                CV2 = "125",
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = _judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }

        [Test]
        public void ADeclinedValidationOnCardPreAuth()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4221690000004963",
                CV2 = "125",
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = _judo.PreAuths.Validate(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Your good to go!", response.Response.ErrorMessage);
            Assert.AreEqual(JudoApiError.Validation_Passed, response.Response.ErrorType);
        }

        [Test]
        public void ASimplePreAuthAndCollection()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = Guid.NewGuid().ToString(),
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "TR14 8PA",
                    Town = "Town"
                }
            };

            var response = _judo.PreAuths.Create(paymentWithCard).Result;

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

            response = _judo.Collections.Create(collection).Result;

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
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = _judo.PreAuths.Create(paymentWithCard).Result;

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

            var validateResponse = _judo.Collections.Validate(collection).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsFalse(validateResponse.HasError);
            Assert.AreEqual("Your good to go!", validateResponse.Response.ErrorMessage);
            Assert.AreEqual(JudoApiError.Validation_Passed, validateResponse.Response.ErrorType);
        }

   

        [Test]
        public void AFailedSimplePreAuthAndValidateCollection()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };

            var response = _judo.PreAuths.Create(paymentWithCard).Result;

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

            var test = _judo.Collections.Create(collection).Result;
            collection.Amount = 30;
            var validateResponse = _judo.Collections.Validate(collection).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsTrue(validateResponse.HasError);
            Assert.True(string.Equals("Sorry, but the amount you're trying to collect is greater than the pre-auth", validateResponse.Error.Message));
            Assert.True(46==validateResponse.Error.Code);
        }


    }
}