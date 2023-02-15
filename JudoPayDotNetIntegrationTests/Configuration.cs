using System.IO;
using JudoPayDotNet.Enums;
using Microsoft.Extensions.Configuration;

namespace JudoPayDotNetIntegrationTests
{
    public class Configuration
    {
        private IConfigurationRoot Config { get; }

        public string WebpaymentsUrl;

        public string Judoid;
        public string CybersourceJudoId;

        public string Token;
        public string Secret;
        
        public string ElevatedPrivilegesToken;
        public string ElevatedPrivilegesSecret;

        public string ThreeDSecure2Token;
        public string ThreeDSecure2Secret;

        public JudoEnvironment JudoEnvironment = JudoEnvironment.Sandbox;
        
        /*
         * Read credentials from file
         */
        public Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true);

            Config = builder.Build();

            WebpaymentsUrl = Config["WebpaymentsUrl"] ?? "https://pay-sandbox.judopay.com/v1/Pay";

            Judoid = Config["Credentials:JudoIds:AI"];
            CybersourceJudoId = Config["Credentials:JudoIds:Cybersource"];

            Token = Config["Credentials:Base:Token"];
            Secret = Config["Credentials:Base:Secret"];

            ElevatedPrivilegesToken = Config["Credentials:ElevatedPrivileges:Token"];
            ElevatedPrivilegesSecret = Config["Credentials:ElevatedPrivileges:Secret"];

            ThreeDSecure2Token = Config["Credentials:ThreeDSecure2:Token"];
            ThreeDSecure2Secret = Config["Credentials:ThreeDSecure2:Secret"];
        }
    }
}