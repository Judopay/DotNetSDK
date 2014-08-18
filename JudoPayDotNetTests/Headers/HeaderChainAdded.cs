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
            const string versionHeader = "api-version-header";
            const string versionHeaderValue = "3.2";

            var testHandler = new TestHandler((request, cancelation) =>
            {
                var requestVersionHeader = request.Headers.FirstOrDefault(h => h.Key == versionHeader);

                Assert.IsNotNull(requestVersionHeader);
                Assert.IsNotNullOrEmpty(requestVersionHeader.Value.FirstOrDefault());
                Assert.AreEqual(versionHeaderValue, requestVersionHeader.Value.FirstOrDefault());

                Assert.AreEqual("Bearer",
                    request.Headers.Authorization.Scheme);
                Assert.AreEqual("ABC", request.Headers.Authorization.Parameter);

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });
            var logger = Substitute.For<ILog>();
            var credentials = new Credentials("ABC");

            var versionHandler = new VersioningHandler(versionHeader, versionHeaderValue);
            var authorizationHandlerhandler = new AuthorizationHandler(credentials, logger);

            var clientWrapper = new HttpClientWrapper(versionHandler, authorizationHandlerhandler, testHandler);

            var client = clientWrapper.HttpClient;

// ReSharper disable once MethodSupportsCancellation
            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}
