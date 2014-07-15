using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Logging;
using JudoPayDotNet.Models.CustomDeserializers;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace JudoPayDotNet.Http
{
    public class Connection
    {
        private readonly IHttpClient _httpClient;
        private readonly Uri _baseAddress;
        private readonly ILog _log;

        //Serialization settings
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            Converters = new JsonConverter[] { new JudoApiErrorModelConverter()}
        };

        public Connection(IHttpClient client, ILog log, string baseAddress)
        {
            _httpClient = client;

            if (!baseAddress.EndsWith("/"))
            {
                baseAddress += "/";
            }

            _baseAddress = new Uri(baseAddress);
            _log = log;
        }

        private HttpRequestMessage BuildRequest(HttpMethod method, string address, 
                                                    Dictionary<string, string> parameters = null,
                                                    object body = null)
        {
            string queryString = string.Empty;

            if (parameters != null && parameters.Count > 0)
            {
                queryString = String.Join("&", parameters.Select(kvp => kvp.Key + "=" + kvp.Value));
            }

            var uri = new UriBuilder(_baseAddress);
            uri.Path += address;
            uri.Query += queryString;

            var request = new HttpRequestMessage(method, uri.Uri);

            if (body != null) 
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body, settings));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return request;
        }

        private async Task<T> HandleResponseCommun<T>(HttpResponseMessage response,
                                                            Func<string, T, T> parser) where T : IResponse, new()
        {
            T parsedResponse = new T();

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.Content.Headers.ContentType.MediaType != "application/json")
            {
                //Not a json response, use a custom error for not accepted response format
                _log.ErrorFormat("Received a response in {0} media type format rather the expected \"application/json\"",
                                    response.Content.Headers.ContentType.MediaType);
                throw new BadResponseError(response);
            }

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        parsedResponse = parser(content, parsedResponse);
                    }
                }
                else
                {
                    //Try to parse an rest well formed error, if it isn't one than an generic http error is thrown
                    try
                    {
                        parsedResponse.JudoError = JsonConvert.DeserializeObject<JudoApiErrorModel>(content, settings);
                    }
                    catch (JsonException e)
                    {
                        _log.ErrorFormat("Ocurred an error deserializing the following content {0}",
                                            e,
                                            content);
                        throw new HttpError(response);
                    }
                }
            }
            catch (JsonException e)
            {
                // If there is an error deserializing a response or an error response then it should be
                // logged and encapsulated on a BadResponseError
                _log.ErrorFormat("Ocurred an error deserializing the following content {0}",
                                            e,
                                            content);
                throw new BadResponseError(e);
            }

            parsedResponse.ResponseBody = content;
            parsedResponse.StatusCode = response.StatusCode;

            return parsedResponse;
        }

        private async Task<HttpResponseMessage> SendCommon(HttpMethod method, string address,
                                        Dictionary<string, string> parameters = null,
                                        object body = null)
        {
            var request = BuildRequest(method, address, parameters, body);
            try
            {
                return await _httpClient.SendAsync(request).ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                //Comunication layer expections are wrapped by HttpRequestException
                throw new ConnectionError(e.InnerException);
            }
            catch (Exception e)
            {
                _log.ErrorFormat("error");
                throw e;
            }
            //TODO: Handle unknown errors propagating a JudoException for the unknown
        }

        private async Task<IResponse> HandleResponse(HttpResponseMessage response)
        {
            // NO OP when the response is not supose to have content
            Func<string, Response, Response> parser = (content, parsedResponse) => parsedResponse;

            return await HandleResponseCommun(response, parser);
        }

        private async Task<IResponse<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            Func<string, Response<T>, Response<T>> parser = (content, parsedResponse) =>
            {
                parsedResponse.ResponseBodyObject = JsonConvert.DeserializeObject<T>(content, settings);
                return parsedResponse;
            };

            return await HandleResponseCommun(response, parser);
        }

        public async Task<IResponse> Send(HttpMethod method, string address,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendCommon(method, address, parameters, body);


            return await HandleResponse(response).ConfigureAwait(false);
        }

        public async Task<IResponse<T>> Send<T>(HttpMethod method, string address,
            Dictionary<string, string> parameters = null,
            object body = null)
        {
            var response = await SendCommon(method, address, parameters, body);

            return await HandleResponse<T>(response).ConfigureAwait(false);
        }
    }
}
