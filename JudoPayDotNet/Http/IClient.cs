using System.Collections.Generic;
using System.Threading.Tasks;
using JudoPayDotNet.Http;

namespace JudoPayDotNet.Client
{
    public interface IClient
    {
        Task<IResponse<T>> Get<T>(string address, Dictionary<string, string> parameters = null);
        Task<IResponse<T>> Post<T>(string address, Dictionary<string, string> parameters = null, 
                                    object body = null);
        Task<IResponse<T>> Update<T>(string address, Dictionary<string, string> parameters = null, 
                                        object body = null);
        Task<IResponse<T>> Delete<T>(string address, Dictionary<string, string> parameters = null, 
                                        object body = null);
    }
}