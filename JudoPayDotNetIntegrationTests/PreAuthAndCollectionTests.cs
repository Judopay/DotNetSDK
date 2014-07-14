using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class PreAuthAndCollectionTests
    {
        [Test]
        public void ASimplePreAuth()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET,
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15"
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            Assert.AreEqual("PreAuth", response.Response.Type);
        }

        [Test]
        public void ASimplePreAuthAndCollection()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET,
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15"
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            Assert.AreEqual("PreAuth", response.Response.Type);

            var collection = new CollectionModel()
            {
                Amount = 25,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            response = judo.Collections.Create(collection).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            Assert.AreEqual("Collection", response.Response.Type);
        }

        [Test]
        public void ASimplePreAuthAndValidateCollection()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET,
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15"
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            Assert.AreEqual("PreAuth", response.Response.Type);

            var collection = new CollectionModel()
            {
                Amount = 25,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var validateResponse = judo.Collections.Validate(collection).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsFalse(validateResponse.HasError);
            Assert.AreEqual("Your good to go!", validateResponse.Response.ErrorMessage);
            Assert.AreEqual(20, validateResponse.Response.ErrorType);
        }

        [Test]
        public void AFailedSimplePreAuthAndValidateCollection()
        {

            var judo = JudoPaymentsFactory.Create(Configuration.TOKEN,
                Configuration.SECRET,
                Configuration.BASEADDRESS);

            var paymentWithCard = new CardPaymentModel()
            {
                JudoId = Configuration.JUDOID,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976000000003436",
                CV2 = "452",
                ExpiryDate = "12/15"
            };

            var response = judo.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
            Assert.AreEqual("PreAuth", response.Response.Type);

            var collection = new CollectionModel()
            {
                Amount = 30,
                ReceiptId = int.Parse(response.Response.ReceiptId),
                YourPaymentReference = "578543"
            };

            var validateResponse = judo.Collections.Validate(collection).Result;

            Assert.IsNotNull(validateResponse);
            Assert.IsTrue(validateResponse.HasError);
            Assert.AreEqual("Unable to process collection as total amount collected would exceed value of" +
                            " original PreAuth transaction.", validateResponse.Error.ErrorMessage);
            Assert.AreEqual(12, validateResponse.Error.ErrorType);
        }
    }
}
