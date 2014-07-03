using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JudoPayDotNet.Errors;
using Newtonsoft.Json;

namespace JudoPayDotNet.Http
{
    public class Connection
    {
        private readonly IHttpClient _httpClient;
        private readonly Uri _baseAddress;

        public Connection(IHttpClient client, string baseAddress)
        {
            _httpClient = client;
            _baseAddress = new Uri(baseAddress);
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

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    parsedResponse.ResponseBodyObject = JsonConvert.DeserializeObject<T>(content);  
                }
            }
            else
            {
                try
                {
                    parsedResponse.JudoError = JsonConvert.DeserializeObject<JudoApiErrorModel>(content);
                }
                // If the error wasn't a judo payments defined error then it should be logged and thrown
                catch (JsonSerializationException)
                {
                    //TODO: Log error behond judo payments control
                    response.EnsureSuccessStatusCode();
                }
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
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            return await HandleResponse<T>(response).ConfigureAwait(false);
        }


    }
}
