using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Autentication;
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
            string versionHeader = "api-version-header";
            string versionHeaderValue = "3.2";

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
            Credentials credentials = new Credentials("ABC");

            VersioningHandler versionHandler = new VersioningHandler(versionHeader, versionHeaderValue);
            AuthorizationHandler authorizationHandlerhandler = new AuthorizationHandler(credentials, logger);

            HttpClientWrapper clientWrapper = new HttpClientWrapper(versionHandler, authorizationHandlerhandler, testHandler);

            HttpClient client = clientWrapper.HttpClient;

            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}
