using System.Configuration;
using JudoPayDotNet.Enums;

namespace JudoPayDotNetIntegrationTests
{
    public class Configuration
    {
        public string WebpaymentsUrl = ConfigurationManager.AppSettings["WebpaymentsUrl"];
        public string Judoid = ConfigurationManager.AppSettings["Judoid"];
        public string ElevatedPrivilegesSecret = ConfigurationManager.AppSettings["ElevatedPrivilegesSecret"];
        public string ElevatedPrivilegesToken = ConfigurationManager.AppSettings["ElevatedPrivilegesToken"];
        public string Token = ConfigurationManager.AppSettings["Token"];
        public string Secret = ConfigurationManager.AppSettings["Secret"];
        public string Cybersource_Judoid = ConfigurationManager.AppSettings["Cybersource_Judoid"];
        public string Cybersource_Token = ConfigurationManager.AppSettings["Cybersource_Token"];
        public string Cybersource_Secret = ConfigurationManager.AppSettings["Cybersource_Secret"];
        public JudoEnvironment JudoEnvironment = JudoEnvironment.Sandbox;
    }
}
