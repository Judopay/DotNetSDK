using JudoPayDotNet;
using JudoPayDotNet.Enums;
using NUnit.Framework;

namespace JudoPayDotNetTests.Configuration
{
    [TestFixture]
    public class JudoSettingsTests
    {
        [Test]
        [TestCase(JudoEnvironment.Sandbox, "https://api-sandbox.judopay.com/")]
        [TestCase(JudoEnvironment.Live, "https://api.judopay.com/")]

        public void GetEnvironmentUrl(JudoEnvironment judoEnvironment, string expectedUrl)
        {
            var result = JudoPaymentsFactory.GetEnvironmentUrl(judoEnvironment);

            Assert.AreEqual(expectedUrl, result);
        }
    }
}
