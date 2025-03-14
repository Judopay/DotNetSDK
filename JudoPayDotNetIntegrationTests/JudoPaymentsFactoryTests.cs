using JudoPayDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    using JudoPayDotNet.Authentication;

    public class JudoPaymentsFactoryTests : IntegrationTestsBase
    {
        [Test]
        public void TestProjectOnlyCreateMethod()
        {
            // Given I create a new Judo client with a custom version number
            var judo = JudoPaymentsFactory.Create(new Credentials(Configuration.Token, Configuration.Secret), Configuration.JudoEnvironment, "6.0");

            var paymentWithCard = GetCardPaymentModel("432438862");

            // When I make a call to an api endpoint
            var response = judo.Payments.Create(paymentWithCard).Result;

            // Then I end up getting a response back
            Assert.IsNotNull(response);
            Assert.AreEqual("Success", response.Response.Result);
        }
    }
}
