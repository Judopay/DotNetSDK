using System;
using JudoPayDotNet;
using JudoPayDotNet.Autentication;
using JudoPayDotNet.Http;
using JudoPayDotNetAndroid.Logging;
using JudoPayDotNet.Client;

namespace JudoPayDotNetAndroid
{
	public static class JudoPaymentsFactory
	{
		public static JudoPayments Create(string token, string secret, string address)
		{
			Credentials credentials = new Credentials(token, secret);
			HttpClientWrapper httpClient = new HttpClientWrapper();
			Connection connection = new Connection(httpClient,
				AndroidLoggerFactory.Create(typeof(Connection)),  
				address);
			Client client = new Client(connection);

			return new JudoPayments(credentials, client);
		}
	}
}

