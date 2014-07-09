using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using JudoPayDotNet.Logging;
using Newtonsoft.Json;

namespace JudoPayDotNet.Http
{
    public class Connection
    {
        private readonly IHttpClient _httpClient;
        private readonly Uri _baseAddress;
        private readonly ILog _log;

        public Connection(IHttpClient client, ILog log, string baseAddress)
        {
            _httpClient = client;
            _baseAddress = new Uri(baseAddress);
            _log = log;
        }

        private HttpRequestMessage BuildRequest(HttpMethod method, string address, 
                                                    Dictionary<string, string> parameters = null,
                                                    object body = null)
        {
            if (parameters != null && parameters.Count > 0)
            {
                var query = String.Join("&", parameters.Select(kvp => kvp.Key + "=" + kvp.Value));
                address += "?" + query;
            }

            var uri = new Uri(_baseAddress, address);
            var request = new HttpRequestMessage(method, uri);

            if (body != null) 
            { 
                request.Content = new StringContent(JsonConvert.SerializeObject(body));
            }

            return request;
        }

        private async Task<Response<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            var parsedResponse = new Response<T>();

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
                        parsedResponse.ResponseBodyObject = JsonConvert.DeserializeObject<T>(content);  
                    }
                }
                else
                {
                    //Try to parse an rest well formed error, if it isn't one than an generic http error is thrown
                    try
                    {
                        parsedResponse.JudoError = JsonConvert.DeserializeObject<JudoApiErrorModel>(content);
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

        public async Task<IResponse<T>> Send<T>(HttpMethod method, string address, 
                                        Dictionary<string, string> parameters = null,  
                                        object body = null)
        {
            var request = BuildRequest(method, address, parameters, body);
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            }
            catch (HttpRequestException e)
            {
                //Comunication layer expections are wrapped by HttpRequestException
                throw new ConnectionError(e.InnerException);
            }
            //TODO: Handle unknown errors propagating a JudoException for the unknown

            return await HandleResponse<T>(response).ConfigureAwait(false);
        }
    }
}
