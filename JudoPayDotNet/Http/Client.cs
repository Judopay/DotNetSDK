using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JudoPayDotNet.Http
{
    public class Client : IClient
    {
        private Connection connection;

        public Client(Connection connection)
        {
            this.connection = connection;
        }

        public Task<IResponse<T>> Get<T>(string address, Dictionary<string, string> parameters = null)
        {
            return connection.Send<T>(HttpMethod.Get, address, parameters);
        }

        public Task<IResponse<T>> Post<T>(string address, Dictionary<string, string> parameters = null, 
                                            object body = null)
        {
            return connection.Send<T>(HttpMethod.Post, address, parameters, body);
        }

        public Task<IResponse> Post(string address, Dictionary<string, string> parameters = null,
                                            object body = null)
        {
            return connection.Send(HttpMethod.Post, address, parameters, body);
        }

        public Task<IResponse<T>> Update<T>(string address, Dictionary<string, string> parameters = null, 
                                                object body = null)
        {
            return connection.Send<T>(HttpMethod.Put, address, parameters, body);
        }

        public Task<IResponse> Update(string address, Dictionary<string, string> parameters = null,
                                                object body = null)
        {
            return connection.Send(HttpMethod.Put, address, parameters, body);
        }

        public Task<IResponse<T>> Delete<T>(string address, Dictionary<string, string> parameters = null, 
                                            object body = null)
        {
            return connection.Send<T>(HttpMethod.Delete, address, parameters);
        }

        public Task<IResponse> Delete(string address, Dictionary<string, string> parameters = null,
                                            object body = null)
        {
            return connection.Send(HttpMethod.Delete, address, parameters);
        }
    }
}
