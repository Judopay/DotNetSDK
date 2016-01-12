using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Enums;
using JudoPayDotNetDotNet;
using JudoPayDotNetDotNet.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Configuration
{
    [TestFixture]
    public class JudoSettingsTests
    {
        [TestFixture]
        public class GetEnvironment
        {
            [Test]
            [TestCase(JudoEnvironment.Sandbox, "https://gw1.judopay-sandbox.com/")]
            [TestCase(JudoEnvironment.Live, "https://gw1.judopay.com/")]

            public void UsingDefault(JudoEnvironment judoEnvironment, string expectedUrl)
            {
                var configuration = Substitute.For<IJudoConfiguration>();

                var result = JudoPaymentsFactory.GetEnvironmentUrl(judoEnvironment, configuration);

                Assert.AreEqual(expectedUrl, result);
            }

            [Test]
            [TestCase(JudoEnvironment.Sandbox, "SandboxUrl", "sandbox")]
            [TestCase(JudoEnvironment.Live, "LiveUrl", "live")]
            public void UsingConfigSetting(JudoEnvironment judoEnvironment, string configKey, string expectedUrl)
            {
                var configuration = Substitute.For<IJudoConfiguration>();
                configuration[configKey].Returns(expectedUrl);

                var result = JudoPaymentsFactory.GetEnvironmentUrl(judoEnvironment, configuration);

                Assert.AreEqual(expectedUrl, result);
            }
        }
    }
}
