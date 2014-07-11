using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    internal abstract class JudoPayClient
    {
        protected readonly IClient Client;
        protected readonly string Address;

        protected JudoPayClient(IClient client, string address)
        {
            Client = client;
            Address = address;
        }

        protected void AddParameter(Dictionary<string, string> parameters, string key, object value)
        {
            string stringValue = value == null ? String.Empty : value.ToString();

            if (!string.IsNullOrEmpty(stringValue) && !parameters.ContainsKey(key))
            {
                parameters.Add(key, stringValue);
            }
        }

        protected async Task<IResult<R>> CreateInternal<T,R>(T entity) where T : class
                                                                where R : class
        {
            R result = null;

            var response = await Client.Post<R>(Address, body: entity).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<R>(result, response.JudoError);
        }

        protected async Task<IResult<T>> GetInternal<T>(string address, 
            Dictionary<string, string> parameters = null) where T : class
        {
            T result = null;

            var response = await Client.Get<T>(address, parameters).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<T>(result, response.JudoError);
        }
    }
}
