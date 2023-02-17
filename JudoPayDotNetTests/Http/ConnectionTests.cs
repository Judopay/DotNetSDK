using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using JudoPayDotNet.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Http
{
    using JudoPayDotNet.Models;

    using NSubstitute.Core;

    [TestFixture]
    public class ConnectionTests
    {
        [Test]
        public void HandleAn400()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent("errorMessage") };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(Task.FromResult(response));
            var connection = new Connection(httpClient, DotNetLoggerFactory.Create, "http://test.com");
            var generatedException = false;

            try
            {
                connection.Send<string>(HttpMethod.Get, "/thisIsATest").Wait();
            }
            catch (AggregateException e)
            {
                var aggregatedException = e.Flatten();
                Assert.AreEqual(1, aggregatedException.InnerExceptions.Count);
                var httpError = aggregatedException.InnerExceptions.FirstOrDefault();
                Assert.IsInstanceOf<HttpError>(httpError);
                // ReSharper disable once PossibleNullReferenceException
                Assert.AreEqual("Status Code : BadRequest, with content: errorMessage", httpError.Message);
                generatedException = true;
            }

            if (!generatedException)
            {
                Assert.Fail("Send should have thrown an HttpError exception");
            }
        }

        [Test]
        public void HandleAnResponseWithDifferentContentType()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("errorMessage") };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/text");
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(Task.FromResult(response));
            var connection = new Connection(httpClient, DotNetLoggerFactory.Create, "http://test.com");
            var generatedException = false;

            try
            {
                connection.Send<string>(HttpMethod.Get, "/thisIsATest").Wait();
            }
            catch (AggregateException e)
            {
                var aggregatedException = e.Flatten();
                Assert.AreEqual(1, aggregatedException.InnerExceptions.Count);
                var badResponseError = aggregatedException.InnerExceptions.FirstOrDefault();
                Assert.IsInstanceOf<BadResponseError>(badResponseError);
                Assert.IsNotNull(badResponseError);
                Assert.AreEqual("Response format isn't valid it should have been application/json but was application/text", badResponseError.Message);
                generatedException = true;
            }

            if (!generatedException)
            {
                Assert.Fail("Send should have thrown an HttpError exception");
            }
        }

        [Test]
        public void HandleConnectionError()
        {
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.When(x => x.SendAsync(Arg.Any<HttpRequestMessage>()))
                .Do(callInfo => { throw new HttpRequestException("A problem reaching destination", new Exception("Unreachable host")); });
            var connection = new Connection(httpClient, DotNetLoggerFactory.Create, "http://test.com");
            var generatedException = false;

            try
            {
                connection.Send<string>(HttpMethod.Get, "/thisIsATest").Wait();
            }
            catch (AggregateException e)
            {
                var aggregatedException = e.Flatten();
                Assert.AreEqual(1, aggregatedException.InnerExceptions.Count);
                var connectionError = aggregatedException.InnerExceptions.FirstOrDefault();
                Assert.IsInstanceOf<ConnectionError>(connectionError);
                Assert.IsNotNull(connectionError);
                Assert.AreEqual("Unreachable host", connectionError.Message);
                generatedException = true;
            }

            if (!generatedException)
            {
                Assert.Fail("Send should have thrown an HttpError exception");
            }
        }
    }
}
