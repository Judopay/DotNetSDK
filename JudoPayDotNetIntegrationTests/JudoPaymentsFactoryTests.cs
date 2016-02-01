using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class JudoPaymentsFactoryTests
    {
        [Test]
        public void TestProjectOnlyCreateMethod()
        {
            // Given I create a new Judo client with a custom version number
            var judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress,
                "4.0");

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

            // When I make a call to an api endpoint
            var response = judo.Payments.Create(paymentWithCard).Result;

            // Then I end up getting a response back
            Assert.IsNotNull(response);
            Assert.AreEqual("Success", response.Response.Result);
        }
    }
}
