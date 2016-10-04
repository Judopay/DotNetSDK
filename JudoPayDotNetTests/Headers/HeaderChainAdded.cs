using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JudoPayDotNet.Authentication;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Headers
{
    [TestFixture]
    public class HeaderChainAdded
    {
        [Test]
        public void VerifyTheExistanceOfAuthenticationAndVersioning()
        {
            const string VERSION_HEADER = "api-version-header";
            const string VERSION_HEADER_VALUE = "3.2";

            var testHandler = new TestHandler((request, cancelation) =>
            {
                var requestVersionHeader = request.Headers.FirstOrDefault(h => h.Key == VERSION_HEADER);

                Assert.IsNotNull(requestVersionHeader);
                Assert.That(requestVersionHeader.Value.FirstOrDefault(), Is.Not.Null.Or.Empty);
                Assert.AreEqual(VERSION_HEADER_VALUE, requestVersionHeader.Value.FirstOrDefault());

                Assert.AreEqual("Bearer",
                    request.Headers.Authorization.Scheme);
                Assert.AreEqual("ABC", request.Headers.Authorization.Parameter);

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });
            var logger = Substitute.For<ILog>();
            var credentials = new Credentials("ABC");

            var versionHandler = new VersioningHandler(VERSION_HEADER, VERSION_HEADER_VALUE);
            var authorizationHandlerhandler = new AuthorizationHandler(credentials, logger);

            var clientWrapper = new HttpClientWrapper(versionHandler, authorizationHandlerhandler, testHandler);

            var client = clientWrapper.HttpClient;

            // ReSharper disable once MethodSupportsCancellation
            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}
