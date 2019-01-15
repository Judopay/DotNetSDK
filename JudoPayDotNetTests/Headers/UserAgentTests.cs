using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace JudoPayDotNetTests.Headers
{
    [TestFixture]
    public class UserAgentTests
    {
        [TestCase("DotNetSDK/1.0.0.0", 1)]
        [TestCase("Mozilla / 4.0", 1)]
        [TestCase("Mozilla / 4.0(compatible)", 0)] // The underlying .Net classes are unable to parse these values
        [TestCase("Mozilla / 4.0(compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)", 0)] // The underlying .Net classes are unable to parse these values
        public void UserAgentFormatTests(string userAgent, int expectedCount)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://foo");
            request.Headers.UserAgent.TryParseAdd(userAgent);

            Assert.That(request.Headers.UserAgent, Has.Count.EqualTo(expectedCount));
        }

        [Test]
        public void OsVersionHeaderTest()
        {
            var platformUserAgent = new ProductInfoHeaderValue(Environment.OSVersion.Platform.ToString(), Environment.OSVersion.Version.ToString());

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Assert.That(platformUserAgent.Product.Name, Does.Contain("Win"));
            }
            else
            {
                Assert.That(platformUserAgent.Product.Name, Does.Contain("Unix"));
            }
        }
    }
}
