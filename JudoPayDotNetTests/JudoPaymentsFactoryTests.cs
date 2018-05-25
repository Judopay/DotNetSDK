using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNetTests
{
    using System.Net.Http.Headers;

    using JudoPayDotNet.Authentication;
    using JudoPayDotNet.Enums;

    using JudoPayDotNet;

    using NUnit.Framework;

    [TestFixture]
    public class JudoPaymentsFactoryTests
    {
        [Test]
        public void PassingNoUserAgentDoesntCauseException()
        {
            var client = JudoPaymentsFactory.Create(new Credentials("abc", "def"), "http://foo", "5.0.0.0");

            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public void PassingDuplicateAgentDoesntCauseException()
        {
            var client = JudoPaymentsFactory.Create(JudoEnvironment.Sandbox, "abc", "def", new ProductInfoHeaderValue("DotNetSDK", "1.0.0.0"));

            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public void TokenSecretUrlConstructorCreatesCLient()
        {
            var client = JudoPaymentsFactory.Create("token", "secret", "http://judo");

            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public void PassingBaseAndVersionCorrectlyCreatesClient()
        {
            var credentials = new Credentials("token", "secret");
            var expectedBaseAddress = "http://test.judopay.com/";
            var client = JudoPaymentsFactory.Create(credentials, expectedBaseAddress, "4.1.0");

            Assert.That(client, Is.Not.Null);
            Assert.That(client.Connection.BaseAddress.ToString(), Is.EqualTo(expectedBaseAddress));
        }
    }
}
