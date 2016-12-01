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
        private const string API_VERSION = "7.2";
        private const string SDK_VERSION = "TEST-7.2";

        private VersioningHandler _versionHandler;

        private AuthorizationHandler _authorizationHandlerhandler;

        [SetUp]
        public void SetupOnce()
        {
            var logger = Substitute.For<ILog>();
            var credentials = new Credentials("ABC");

            _versionHandler = new VersioningHandler(API_VERSION, SDK_VERSION);
            _authorizationHandlerhandler = new AuthorizationHandler(credentials, logger);
        }

        [Test]
        public void VerifyTheExistanceOfApiVersioning()
        {
            var testHandler = new TestHandler(
                                  (request, cancelation) =>
                                      {
                                          var requestVersionHeader = request.Headers.FirstOrDefault(h => h.Key == VersioningHandler.API_VERSION_HEADER);

                                          // Ensure API version header is sent
                                          Assert.IsNotNull(requestVersionHeader);
                                          Assert.That(requestVersionHeader.Value.FirstOrDefault(), Is.Not.Null.Or.Empty);
                                          Assert.AreEqual(API_VERSION, requestVersionHeader.Value.FirstOrDefault());

                                          return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                                      });

            var clientWrapper = new HttpClientWrapper(_versionHandler, _authorizationHandlerhandler, testHandler);

            var client = clientWrapper.HttpClient;

            // ReSharper disable once MethodSupportsCancellation
            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Test]
        public void VerifyTheExistanceOfSDKVersioning()
        {
            var testHandler = new TestHandler(
                                  (request, cancelation) =>
                                  {
                                      var requestVersionHeader = request.Headers.FirstOrDefault(h => h.Key == VersioningHandler.SDK_VERSION_HEADER);

                                      // Ensure API version header is sent
                                      Assert.IsNotNull(requestVersionHeader);
                                      Assert.That(requestVersionHeader.Value.FirstOrDefault(), Is.Not.Null.Or.Empty);
                                      Assert.That(requestVersionHeader.Value.FirstOrDefault(), Contains.Substring(SDK_VERSION));
                                      Assert.That(requestVersionHeader.Value.FirstOrDefault(), Contains.Substring("DotNetSDK-"));

                                      return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                                  });

            var clientWrapper = new HttpClientWrapper(_versionHandler, _authorizationHandlerhandler, testHandler);

            var client = clientWrapper.HttpClient;

            // ReSharper disable once MethodSupportsCancellation
            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Test]
        public void VerifyTheExistanceOfAuthenticationHeader()
        {
            var testHandler = new TestHandler(
                                  (request, cancelation) =>
                                  {
                                      // Ensure that authentication header is sent
                                      Assert.AreEqual("Bearer", request.Headers.Authorization.Scheme);
                                      Assert.AreEqual("ABC", request.Headers.Authorization.Parameter);

                                      return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                                  });

            var clientWrapper = new HttpClientWrapper(_versionHandler, _authorizationHandlerhandler, testHandler);

            var client = clientWrapper.HttpClient;

            // ReSharper disable once MethodSupportsCancellation
            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }
    }
}
