using System.Configuration;
using JudoPayDotNet.Enums;

namespace JudoPayDotNetIntegrationTests
{
    public class Configuration
    {
        public string WebpaymentsUrl = "https://pay.judopay-sandbox.com/v1/Pay";
        public string Judoid = "100915867";
        public string ElevatedPrivilegesSecret = "cc9274f05a6e558f4ed8be5ad0429f7edbb05a594d56f851c2b8a53fedfce3d9";
        public string ElevatedPrivilegesToken = "90GEiHrgjEuHnbAt";
        public string Token = "Izx9omsBR15LatAl";
        public string Secret = "b5787124845533d8e68d12a586fa3713871b876b528600ebfdc037afec880cd6";
        public string Cybersource_Judoid = "100986934";
        public string Cybersource_Token = "os92LSyQe0Ywp2tE";
        public string Cybersource_Secret = "d6110e7becd1a4f4b8d5d00e8cefbbf4342bf981270ea79325d73e68fb6e7d23";
        public JudoEnvironment JudoEnvironment = JudoEnvironment.Sandbox;
    }
}
