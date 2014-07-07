using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Http;
using NSubstitute;
using NUnit.Framework;

namespace JudoPayDotNetTests.Http
{
    [TestFixture]
    public class ConnectionTests
    {
        [Test]
        public void HandleAn400()
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent("errorMessage");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(Task.FromResult(response));
            Connection connection = new Connection(httpClient, "http://test.com");
            bool generatedException =false;

            try
            {
                var result = connection.Send<string>(HttpMethod.Get, "/thisIsATest").Result;
            }
            catch (AggregateException e)
            {
                var aggregatedException = e.Flatten();
                Assert.AreEqual(1, aggregatedException.InnerExceptions.Count);
                var httpError = aggregatedException.InnerExceptions.FirstOrDefault();
                Assert.IsInstanceOf<HttpError>(httpError);
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
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            response.Content = new StringContent("errorMessage");
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/text");
            var httpClient = Substitute.For<IHttpClient>();
            httpClient.SendAsync(Arg.Any<HttpRequestMessage>()).Returns(Task.FromResult(response));
            Connection connection = new Connection(httpClient, "http://test.com");
            bool generatedException = false;

            try
            {
                var result = connection.Send<string>(HttpMethod.Get, "/thisIsATest").Result;
            }
            catch (AggregateException e)
            {
                var aggregatedException = e.Flatten();
                Assert.AreEqual(1, aggregatedException.InnerExceptions.Count);
                var badResponseError = aggregatedException.InnerExceptions.FirstOrDefault();
                Assert.IsInstanceOf<BadResponseError>(badResponseError);
                Assert.AreEqual("Response format isn't valid it should have been application/json but was application/text", 
                                badResponseError.Message);
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
            httpClient.When(x => x.SendAsync(Arg.Any<HttpRequestMessage>())).
                Do(callInfo => { throw new HttpRequestException("A problem reaching destination",
                                                new Exception("Unreachable host"));
                });
            Connection connection = new Connection(httpClient, "http://test.com");
            bool generatedException = false;

            try
            {
                var result = connection.Send<string>(HttpMethod.Get, "/thisIsATest").Result;
            }
            catch (AggregateException e)
            {
                var aggregatedException = e.Flatten();
                Assert.AreEqual(1, aggregatedException.InnerExceptions.Count);
                var connectionError = aggregatedException.InnerExceptions.FirstOrDefault();
                Assert.IsInstanceOf<ConnectionError>(connectionError);
                Assert.AreEqual("Unreachable host",
                                connectionError.Message);
                generatedException = true;
            }

            if (!generatedException)
            {
                Assert.Fail("Send should have thrown an HttpError exception");
            }
        }
    }
}
