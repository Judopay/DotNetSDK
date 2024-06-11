using System;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using NSubstitute;

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
            var client = JudoPaymentsFactory.Create(credentials, expectedBaseAddress, "4.2.0");

            Assert.That(client, Is.Not.Null);
            Assert.That(client.Connection.BaseAddress.ToString(), Is.EqualTo(expectedBaseAddress));
        }


        [Test]
        [TestCase(JudoPaymentsFactory.DefaultLiveUrl, JudoPaymentsFactory.PrimaryLivePublicKey)]
        [TestCase(JudoPaymentsFactory.DefaultLiveUrl, JudoPaymentsFactory.FallbackLivePublicKey)]
        [TestCase(JudoPaymentsFactory.LegacyLiveUrl, JudoPaymentsFactory.PrimaryLivePublicKey)]
        [TestCase(JudoPaymentsFactory.LegacyLiveUrl, JudoPaymentsFactory.FallbackLivePublicKey)]
        [TestCase(JudoPaymentsFactory.DefaultSandboxUrl, JudoPaymentsFactory.PrimaryLivePublicKey)]
        [TestCase(JudoPaymentsFactory.DefaultSandboxUrl, JudoPaymentsFactory.FallbackLivePublicKey)]
        [TestCase(JudoPaymentsFactory.LegacySandboxUrl, JudoPaymentsFactory.PrimaryLegacyPublicKey)]
        [TestCase(JudoPaymentsFactory.LegacySandboxUrl, JudoPaymentsFactory.FallbackLegacyPublicKey)]
        public void TestCertificatePinningDotNetFrameworkWithValidCert(string baseUrl, string publicKey)
        {
            var sender = WebRequest.Create(baseUrl);
            var certificate = Substitute.For<X509Certificate>();
            certificate.GetPublicKey().Returns(Convert.FromBase64String(publicKey));
            var chain = Substitute.For<X509Chain>();
            var sslPolicyErrors = SslPolicyErrors.None;

            var ret = JudoPaymentsFactory.PinPublicKey(sender, certificate, chain, sslPolicyErrors);
            Assert.That(ret, Is.True);
        }

        [Test]
        [TestCase(JudoPaymentsFactory.DefaultLiveUrl, JudoPaymentsFactory.PrimaryLegacyPublicKey)]
        [TestCase(JudoPaymentsFactory.DefaultLiveUrl, JudoPaymentsFactory.FallbackLegacyPublicKey)]
        public void TestCertificatePinningDotNetFrameworkWithInvalidCert(string baseUrl, string publicKey)
        {
            var sender = WebRequest.Create(baseUrl);
            var certificate = Substitute.For<X509Certificate>();
            certificate.GetPublicKey().Returns(Convert.FromBase64String(publicKey));
            var chain = Substitute.For<X509Chain>();
            var sslPolicyErrors = SslPolicyErrors.None;

            var ret = JudoPaymentsFactory.PinPublicKey(sender, certificate, chain, sslPolicyErrors);
            Assert.That(ret, Is.False);
        }

        [Test]
        [TestCase(JudoPaymentsFactory.DefaultLiveUrl, JudoPaymentsFactory.PrimaryLivePublicKey)]
        [TestCase(JudoPaymentsFactory.DefaultLiveUrl, JudoPaymentsFactory.FallbackLivePublicKey)]
        [TestCase(JudoPaymentsFactory.LegacyLiveUrl, JudoPaymentsFactory.PrimaryLivePublicKey)]
        [TestCase(JudoPaymentsFactory.LegacyLiveUrl, JudoPaymentsFactory.FallbackLivePublicKey)]
        [TestCase(JudoPaymentsFactory.DefaultSandboxUrl, JudoPaymentsFactory.PrimaryLivePublicKey)]
        [TestCase(JudoPaymentsFactory.DefaultSandboxUrl, JudoPaymentsFactory.FallbackLivePublicKey)]
        [TestCase(JudoPaymentsFactory.LegacySandboxUrl, JudoPaymentsFactory.PrimaryLegacyPublicKey)]
        [TestCase(JudoPaymentsFactory.LegacySandboxUrl, JudoPaymentsFactory.FallbackLegacyPublicKey)]
        public void TestCertificatePinningDotNetCoreWithValidCert(string baseUrl, string publicKey)
        {
            var sender = new HttpRequestMessage(HttpMethod.Get, baseUrl);
            var certificate = Substitute.For<X509Certificate>();
            certificate.GetPublicKey().Returns(Convert.FromBase64String(publicKey));
            var chain = Substitute.For<X509Chain>();
            var sslPolicyErrors = SslPolicyErrors.None;

            var ret = JudoPaymentsFactory.PinPublicKey(sender, certificate, chain, sslPolicyErrors);
            Assert.That(ret, Is.True);
        }

        [Test]
        [TestCase(JudoPaymentsFactory.DefaultLiveUrl, JudoPaymentsFactory.PrimaryLegacyPublicKey)]
        [TestCase(JudoPaymentsFactory.DefaultLiveUrl, JudoPaymentsFactory.FallbackLegacyPublicKey)]
        public void TestCertificatePinningDotNetCoreWithInvalidCert(string baseUrl, string publicKey)
        {
            var sender = new HttpRequestMessage(HttpMethod.Get, baseUrl);
            var certificate = Substitute.For<X509Certificate>();
            certificate.GetPublicKey().Returns(Convert.FromBase64String(publicKey));
            var chain = Substitute.For<X509Chain>();
            var sslPolicyErrors = SslPolicyErrors.None;

            var ret = JudoPaymentsFactory.PinPublicKey(sender, certificate, chain, sslPolicyErrors);
            Assert.That(ret, Is.False);
        }

        [Test]
        [TestCase(JudoPaymentsFactory.DefaultLiveUrl)]
        [TestCase(JudoPaymentsFactory.DefaultSandboxUrl)]
        public void TestCertificatePinningWithNullCertificate(string baseUrl)
        {
            var sender = new HttpRequestMessage(HttpMethod.Get, baseUrl);
            var chain = Substitute.For<X509Chain>();
            var sslPolicyErrors = SslPolicyErrors.None;

            var ret = JudoPaymentsFactory.PinPublicKey(sender, null, chain, sslPolicyErrors);
            Assert.That(ret, Is.False);
        }
    }
}
