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

        protected JudoPayClient(IClient client)
        {
            Client = client;
        }

        protected void AddParameter(Dictionary<string, string> parameters, string key, object value)
        {
            string stringValue = value == null ? String.Empty : value.ToString();

            if (!string.IsNullOrEmpty(stringValue) && !parameters.ContainsKey(key))
            {
                parameters.Add(key, stringValue);
            }
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

        protected async Task<IResult<R>> PostInternal<T, R>(string address, T entity, 
                                                                Dictionary<string, string> parameters = null)
            where T : class
                                                                where R : class
        {
            R result = null;

            var response = await Client.Post<R>(address, parameters, entity).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<R>(result, response.JudoError);
        }

        protected async Task<IResult<R>> PutInternal<T, R>(string address,
                                                            T entity, 
                                                            Dictionary<string, string> parameters = null)
            where T : class
            where R : class
        {
            R result = null;

            var response = await Client.Update<R>(address, parameters, entity).ConfigureAwait(false);

            if (!response.ErrorResponse)
            {
                result = response.ResponseBodyObject;
            }

            return new Result<R>(result, response.JudoError);
        }

        protected async Task<IResult> DeleteInternal<T>(string address, 
                                                        T entity, 
                                                        Dictionary<string, string> parameters = null)
            where T : class
        {
            var response = await Client.Delete(address, parameters, entity).ConfigureAwait(false);

            return new Result(response.JudoError);
        }
    }
}
