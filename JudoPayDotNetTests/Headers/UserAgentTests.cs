using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JudoPayDotNetTests.Headers
{
    using System.Net.Http;
    using System.Net.Http.Headers;

    using JudoPayDotNet.Http;

    using NUnit.Framework;

    [TestFixture]
    public class UserAgentTests
    {
        [TestCase("Mozilla / 4.0", 1)]
        [TestCase("Mozilla / 4.0(compatible)", 0)] // The underlying .Net classes are unable to parse these values
        [TestCase("Mozilla / 4.0(compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)", 0)] // The underlying .Net classes are unable to parse these values
        public void UserAgentFormatTests(string userAgent, int expectedCount)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://foo");
            request.Headers.UserAgent.TryParseAdd(userAgent);

            Assert.That(request.Headers.UserAgent, Has.Count.EqualTo(expectedCount));
        }
    }
}
