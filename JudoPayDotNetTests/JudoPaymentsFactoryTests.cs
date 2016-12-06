using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNetTests
{
    using System.Net.Http.Headers;

    using JudoPayDotNet.Authentication;

    using JudoPayDotNetDotNet;

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
            var client = JudoPaymentsFactory.Create("abc", "def", "http://foo", new ProductInfoHeaderValue("DotNetSDK", "1.0.0.0"));

            Assert.That(client, Is.Not.Null);
        }
    }
}
