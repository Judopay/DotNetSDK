using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JudoPayDotNet.Http;
using NUnit.Framework;

namespace JudoPayDotNetTests.Headers
{
    [TestFixture]
    public class VersioningTests
    {
        [Test]
        public void VersionHeaderTest()
        {
            const string versionHeader = "api-version-header";
            const string versionHeaderValue = "3.2";

            var testHandler = new TestHandler((request, cancelation) =>
            {
                var requestVersionHeader = request.Headers.FirstOrDefault(h => h.Key == versionHeader);
                
                Assert.IsNotNull(requestVersionHeader);
                Assert.IsNotNullOrEmpty(requestVersionHeader.Value.FirstOrDefault());
                Assert.AreEqual(versionHeaderValue, requestVersionHeader.Value.FirstOrDefault());

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });

            var handler = new VersioningHandler(versionHeader, versionHeaderValue) {InnerHandler = testHandler};

            var client = new HttpClient(handler);

// ReSharper disable once MethodSupportsCancellation
            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}
