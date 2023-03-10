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
    using System.Collections.Generic;
    using System.Net.Http.Headers;
    using System.Reflection;

    [TestFixture]
    public class HeaderChainAdded
    {
        private const string API_VERSION = "7.2";

        private VersioningHandler _versionHandler;

        private AuthorizationHandler _authorizationHandlerhandler;

        private static readonly string ProductVersion = new AssemblyName(typeof(HttpClientWrapper).GetTypeInfo().Assembly.FullName).Version.ToString();

        [SetUp]
        public void SetupOnce()
        {
            var logger = Substitute.For<ILog>();
            var credentials = new Credentials("ABC");

            _versionHandler = new VersioningHandler(API_VERSION);
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

        [Test]
        public void VerifyExistanceOfDefaultHeaders()
        {
            var testHandler = new TestHandler(
                                  (request, cancelation) =>
                                  {
                                      // Ensure User-Agent is sent
                                      Assert.That(request.Headers.UserAgent, Is.Not.Null.Or.Empty);
                                      Assert.That(request.Headers.UserAgent.First().Product.Name, Is.EqualTo("JudoDotNetSDK"));
                                      Assert.That(request.Headers.UserAgent.First().Product.Version, Is.EqualTo(ProductVersion));

                                      return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                                  });

            var clientWrapper = new HttpClientWrapper(_versionHandler, _authorizationHandlerhandler, testHandler);

            var client = clientWrapper.HttpClient;

            // ReSharper disable once MethodSupportsCancellation
            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Test]
        public void VerifyExistanceOfExplicitUserAgent()
        {
            var customAgent = new List<ProductInfoHeaderValue>() { new ProductInfoHeaderValue("TEST", "123") };

            var testHandler = new TestHandler(
                                  (request, cancelation) =>
                                  {
                                      // Ensure User-Agent is sent
                                      Assert.That(request.Headers.UserAgent, Is.Not.Null.Or.Empty);

                                      Assert.That(request.Headers.UserAgent.Count, Is.EqualTo(2));
                                      Assert.That(request.Headers.UserAgent.Any(a => a.Product.Name == "TEST"));

                                      return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK));
                                  });

            var clientWrapper = new HttpClientWrapper(customAgent, _versionHandler, _authorizationHandlerhandler, testHandler);

            var client = clientWrapper.HttpClient;

            // ReSharper disable once MethodSupportsCancellation
            var response = client.GetAsync("http://lodididki");

            Assert.AreEqual(HttpStatusCode.OK, response.Result.StatusCode);
        }

        [Test]
        public void VerifyNoAdditionalHandlersIsHandledCorrectly()
        {
            var customAgent = new List<ProductInfoHeaderValue>() { new ProductInfoHeaderValue("TEST", "123") };

            var clientWrapper = new HttpClientWrapper(customAgent);

            var client = clientWrapper.HttpClient;

            Assert.That(client, Is.InstanceOf<HttpClient>());
            Assert.That(client.DefaultRequestHeaders.UserAgent.ToString(), Contains.Substring("TEST/123"));
        }
    }
}
