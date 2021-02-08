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
        public string Token;
        public string Secret;

        public string Cybersource_Judoid;
        public string Cybersource_Token;
        public string Cybersource_Secret;

        public string SafeCharge_Judoid;
        public string SafeCharge_Token;
        public string SafeCharge_Secret;

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

            WebpaymentsUrl = Config["WebpaymentsUrl"] ?? "https://pay.judopay-sandbox.com/v1/Pay";

            Judoid = Config["Credentials:Base:JudoId"];
            Token = Config["Credentials:Base:Token"];
            Secret = Config["Credentials:Base:Secret"];

            Cybersource_Judoid = Config["Credentials:Cybersource:JudoId"];
            Cybersource_Token = Config["Credentials:Cybersource:Token"];
            Cybersource_Secret = Config["Credentials:Cybersource:Secret"];

            SafeCharge_Judoid = Config["Credentials:SafeCharge:JudoId"];
            SafeCharge_Token = Config["Credentials:SafeCharge:Token"];
            SafeCharge_Secret = Config["Credentials:SafeCharge:Secret"];
        }
    }
}