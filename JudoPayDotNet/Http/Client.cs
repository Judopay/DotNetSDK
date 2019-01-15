using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    internal class Client : IClient
    {
        public Connection Connection { get; }

        public Client(Connection connection)
        {
            Connection = connection;
        }

        public Task<IResponse<T>> Get<T>(string address, Dictionary<string, string> parameters = null, Dictionary<string, string> extraHeaders = null)
        {
            return Connection.Send<T>(HttpMethod.Get, address, parameters, extraHeaders);
        }

        public Task<IResponse<T>> Post<T>(string address, Dictionary<string, string> parameters = null, Dictionary<string, string> extraHeaders = null, object body = null)
        {
            return Connection.Send<T>(HttpMethod.Post, address, parameters, extraHeaders, body);
        }

        public Task<IResponse> Post(string address, Dictionary<string, string> parameters = null, Dictionary<string, string> extraHeaders = null, object body = null)
        {
            return Connection.Send(HttpMethod.Post, address, parameters, extraHeaders, body);
        }

        public Task<IResponse<T>> Update<T>(string address, Dictionary<string, string> parameters = null, Dictionary<string, string> extraHeaders = null, object body = null)
        {
            return Connection.Send<T>(HttpMethod.Put, address, parameters, extraHeaders, body);
        }

        public Task<IResponse> Update(string address, Dictionary<string, string> parameters = null, Dictionary<string, string> extraHeaders = null, object body = null)
        {
            return Connection.Send(HttpMethod.Put, address, parameters, extraHeaders, body);
        }

        public Task<IResponse<T>> Delete<T>(string address, Dictionary<string, string> parameters = null, Dictionary<string, string> extraHeaders = null)
        {
            return Connection.Send<T>(HttpMethod.Delete, address, parameters, extraHeaders);
        }

        public Task<IResponse> Delete(string address, Dictionary<string, string> parameters = null, Dictionary<string, string> extraHeaders = null)
        {
            return Connection.Send(HttpMethod.Delete, address, parameters, extraHeaders);
        }
    }
}
