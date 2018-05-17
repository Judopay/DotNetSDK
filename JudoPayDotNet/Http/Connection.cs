using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models.CustomDeserializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JudoPayDotNet.Http
{
    using JudoPayDotNet.Models;

    /// <summary>
    /// Handles the http requests creation and the http responses
    /// </summary>
    public class Connection
    {
        private readonly IHttpClient _httpClient;

        internal readonly Uri BaseAddress;

        private readonly ILog _log;

        /// <summary>
        /// Serialization settings
        /// </summary>
        private readonly JsonSerializerSettings _settings;

        public Connection(IHttpClient client, Func<Type, ILog> log, string baseAddress)
        {
            _httpClient = client;

            if (!baseAddress.EndsWith("/"))
            {
                baseAddress += "/";
            }

            BaseAddress = new Uri(baseAddress);
            _log = log(GetType());

            _settings = new JsonSerializerSettings
                            {
                                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                                Converters = new JsonConverter[] { new JudoApiErrorModelConverter(log(typeof(JudoApiErrorModelConverter))), new TransactionResultConvertor() }
                            };
        }

        /// <summary>
        /// Builds our base request.
        /// </summary>
        /// <param name="method">The http method.</param>
        /// <param name="address">The address.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <param name="extraHeaders">If you need to set any additional http headers on the request</param>
        /// <param name="body">The body entity.</param>
        /// <returns>An http request object</returns>
        private HttpRequestMessage BuildRequest(
            HttpMethod method,
            string address,
            Dictionary<string, string> parameters = null,
            Dictionary<string, string> extraHeaders = null,
            object body = null)
        {
            var queryString = string.Empty;

            if (parameters != null && parameters.Count > 0)
            {
                queryString = string.Join("&", parameters.Select(kvp => kvp.Key + "=" + kvp.Value));
            }

            var uri = new UriBuilder(BaseAddress);
            uri.Path += address;
            uri.Query += queryString;

            var request = new HttpRequestMessage(method, uri.Uri);

            if (extraHeaders != null)
            {
                foreach (var header in extraHeaders)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            var paymentModel = body as PaymentModel;
            if (paymentModel != null && !string.IsNullOrWhiteSpace(paymentModel.UserAgent))
            {
                request.Headers.UserAgent.TryParseAdd(paymentModel.UserAgent);
            }
            
            if (body != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body, _settings), new UTF8Encoding());
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return request;
        }

        /// <summary>
        /// Handles the response. Checking for errors and parsing the response to the expected model
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        /// <param name="response">The http response.</param>
        /// <param name="parser">The function responsible for parsing response.</param>
        /// <returns>The handled response with result and error if something wrong happened</returns>
        /// <exception cref="BadResponseError">
        /// Response in wrong format or malformed
        /// </exception>
        /// <exception cref="HttpError">Generic http error</exception>
        private async Task<T> HandleResponseCommon<T>(HttpResponseMessage response, Func<string, T, T> parser) where T : IResponse, new()
        {
            var parsedResponse = new T();

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (response.Content.Headers.ContentType.MediaType != "application/json")
                        {
                            //Not a json response, use a custom error for not accepted response format
                            _log.ErrorFormat("Received a response in {0} media type format rather the expected \"application/json\"", response.Content.Headers.ContentType.MediaType);
                            throw new BadResponseError(response);
                        }

                        parsedResponse = parser(content, parsedResponse);
                    }
                }
                else
                {
                    //Try to parse an rest well formed error, if it isn't one than an generic http error is thrown
                    try
                    {
                        parsedResponse.JudoError = JsonConvert.DeserializeObject<ModelError>(content, _settings);
                    }
                    catch (JsonException e)
                    {
                        _log.ErrorFormat("Ocurred an error deserializing the following content {0}", e, content);
                        throw new HttpError(response);
                    }
                }
            }
            catch (JsonException e)
            {
                // If there is an error deserializing a response or an error response then it should be
                // logged and encapsulated on a BadResponseError
                _log.ErrorFormat("An error occurred deserializing the following content {0}", e, content);
                throw new BadResponseError(e);
            }

            parsedResponse.ResponseBody = content;
            parsedResponse.StatusCode = response.StatusCode;

            return parsedResponse;
        }

        /// <summary>
        /// Implements the process of constructing the request and sending it using http.
        /// </summary>
        /// <param name="method">The http method.</param>
        /// <param name="address">The URI address.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <param name="body">The entity body.</param>
        /// <returns>Http response to be handled</returns>
        /// <exception cref="ConnectionError">When something wrong happens at the connection level</exception>
        private async Task<HttpResponseMessage> SendCommon(
            HttpMethod method,
            string address,
            Dictionary<string, string> parameters = null,
            Dictionary<string, string> extraHeaders = null,
            object body = null)
        {
            var request = BuildRequest(method, address, parameters, extraHeaders, body);
            try
            {
                return await _httpClient.SendAsync(request).ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                _log.Error("Http error", e.InnerException);

                // Communication layer expections are wrapped by HttpRequestException
                throw new ConnectionError(e.InnerException);
            }
            catch (Exception e)
            {
                _log.Error("Generic error on communication layer", e);
                throw;
            }
        }

        /// <summary>
        /// Handles the http response.
        /// </summary>
        /// <param name="response">The http response.</param>
        /// <returns>The response parsed and with error if something wrong happend</returns>
        private async Task<IResponse> HandleResponse(HttpResponseMessage response)
        {
            // NO OP when the response is not supposed to have content
            Func<string, Response, Response> parser = (content, parsedResponse) => parsedResponse;

            return await HandleResponseCommon(response, parser);
        }

        /// <summary>
        /// Handles the http response.
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        /// <param name="response">The http response.</param>
        /// <returns>The response parsed and with error if something wrong happend</returns>
        private async Task<IResponse<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            Func<string, Response<T>, Response<T>> parser = (content, parsedResponse) =>
                {
                    parsedResponse.ResponseBodyObject = JsonConvert.DeserializeObject<T>(content, _settings);
                    return parsedResponse;
                };

            return await HandleResponseCommon(response, parser);
        }

        /// <summary>
        /// Creates an http request, sends it to the server and handle the response checking for errors and parsing it.
        /// </summary>
        /// <param name="method">The http method.</param>
        /// <param name="address">The URI address.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <param name="body">The entity body.</param>
        /// <returns>The response parsed and with error if something wrong happend</returns>
        public async Task<IResponse> Send(HttpMethod method, string address, Dictionary<string, string> parameters = null, Dictionary<string, string> extraHeaders = null, object body = null)
        {
            var response = await SendCommon(method, address, parameters, extraHeaders, body).ConfigureAwait(false);


            return await HandleResponse(response).ConfigureAwait(false);
        }

        /// <summary>
        /// Creates an http request, sends it to the server and handle the response checking for errors and parsing it.
        /// </summary>
        /// <typeparam name="T">Result type</typeparam>
        /// <param name="method">The http method.</param>
        /// <param name="address">The URI address.</param>
        /// <param name="parameters">The query string parameters.</param>
        /// <param name="extraHeaders">Any extra http headers to send with the request</param>
        /// <param name="body">The entity body.</param>
        /// <returns>The response parsed and with error if something wrong happend</returns>
        public async Task<IResponse<T>> Send<T>(
            HttpMethod method,
            string address,
            Dictionary<string, string> parameters = null,
            Dictionary<string, string> extraHeaders = null,
            object body = null)
        {
            var response = await SendCommon(method, address, parameters, extraHeaders, body).ConfigureAwait(false);

            return await HandleResponse<T>(response).ConfigureAwait(false);
        }
    }
}
