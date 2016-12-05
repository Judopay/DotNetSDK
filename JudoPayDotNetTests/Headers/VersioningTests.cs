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
            const string versionHeaderValue = "3.2";

            var testHandler = new TestHandler(
                                  (request, cancelation) =>
                                      {
                                          var requestVersionHeader = request.Headers.FirstOrDefault(h => h.Key == VersioningHandler.API_VERSION_HEADER);

                                          Assert.IsNotNull(requestVersionHeader);
                                          Assert.That(requestVersionHeader.Value.FirstOrDefault(), Is.Not.Null.Or.Empty);
                                          Assert.That(requestVersionHeader.Value.FirstOrDefault(), Is.EqualTo(versionHeaderValue));

                                          return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                                      });

            var handler = new VersioningHandler(versionHeaderValue) { InnerHandler = testHandler };

            var client = new HttpClient(handler);

            // ReSharper disable once MethodSupportsCancellation
            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}
