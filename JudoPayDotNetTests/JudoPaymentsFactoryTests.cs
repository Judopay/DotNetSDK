using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNetTests
{
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
    }
}
