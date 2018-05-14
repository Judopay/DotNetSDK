using JudoPayDotNet;
using JudoPayDotNet.Enums;
using NUnit.Framework;

namespace JudoPayDotNetTests.Configuration
{
    [TestFixture]
    public class JudoSettingsTests
    {
        [Test]
        [TestCase(JudoEnvironment.Sandbox, "https://gw1.judopay-sandbox.com/")]
        [TestCase(JudoEnvironment.Live, "https://gw1.judopay.com/")]

        public void GetEnvironmentUrl(JudoEnvironment judoEnvironment, string expectedUrl)
        {
            var result = JudoPaymentsFactory.GetEnvironmentUrl(judoEnvironment);

            Assert.AreEqual(expectedUrl, result);
        }
    }
}
