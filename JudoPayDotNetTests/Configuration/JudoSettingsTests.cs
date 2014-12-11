using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNetDotNet;
using JudoPayDotNetDotNet.Configuration;
using NSubstitute;
using NUnit.Framework;
using Environment = JudoPayDotNet.Enums.Environment;

namespace JudoPayDotNetTests.Configuration
{
    [TestFixture]
    public class JudoSettingsTests
    {
        [TestFixture]
        public class GetEnvironment
        {
            [Test]
            [TestCase(Environment.Sandbox,"https://partnerapi.judopay-sandbox.com/")]
            [TestCase(Environment.Live, "https://partnerapi.judopay.com/")]
            public void UsingDefault(Environment environment, string expectedUrl)
            {
                var configuration = Substitute.For<IJudoConfiguration>();

                var result = JudoPaymentsFactory.GetEnvironmentUrl(environment, configuration);

                Assert.AreEqual(expectedUrl, result);
            }

            [Test]
            [TestCase(Environment.Sandbox, "SandboxUrl", "sandbox")]
            [TestCase(Environment.Live, "LiveUrl", "live")]
            public void UsingConfigSetting(Environment environment, string configKey, string expectedUrl)
            {
                var configuration = Substitute.For<IJudoConfiguration>();
                configuration[configKey].Returns(expectedUrl);

                var result = JudoPaymentsFactory.GetEnvironmentUrl(environment, configuration);

                Assert.AreEqual(expectedUrl, result);
            }
        }
    }
}
