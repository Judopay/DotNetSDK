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
        public JudoEnvironment JudoEnvironment = JudoEnvironment.Sandbox;
    }
}
