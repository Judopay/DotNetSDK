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

namespace JudoPayDotNetTests.Authorization
{
    [TestFixture]
    public class AuthorizationHandlerTests
    {
        [Test]
        public void AuthorizeWithOAuth()
        {
            var logger = Substitute.For<ILog>();
            var testHandler = new TestHandler((request, cancelation) =>
            {
                Assert.AreEqual("Bearer",
                    request.Headers.Authorization.Scheme);
                Assert.AreEqual("ABC", request.Headers.Authorization.Parameter);

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });
            Credentials credentials = new Credentials("ABC");

            AuthorizationHandler handler = new AuthorizationHandler(credentials, logger);

            handler.InnerHandler = testHandler;

            HttpClient client = new HttpClient(handler);

            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Test]
        public void AuthorizeWithBasicAuthentication()
        {
            var logger = Substitute.For<ILog>();
            var testHandler = new TestHandler((request, cancelation) =>
            {
                Assert.AreEqual("Basic",
                    request.Headers.Authorization.Scheme);
                var authDetailsBytes = Convert.FromBase64String(request.Headers.Authorization.Parameter);
                var authDetails = Encoding.GetEncoding("iso-8859-1").GetString(authDetailsBytes);

                var authArray = authDetails.Split(':');

                Assert.AreEqual("testToken", authArray[0]);
                Assert.AreEqual("testSecret", authArray[1]);

                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
            });
            Credentials credentials = new Credentials("testToken", "testSecret");

            AuthorizationHandler handler = new AuthorizationHandler(credentials, logger);

            handler.InnerHandler = testHandler;

            HttpClient client = new HttpClient(handler);

            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}
