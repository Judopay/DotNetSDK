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
        public string Cybersource_Judoid = "100882208";
        public string Cybersource_Token = "uYmqYs9nicZWbBUu";
        public string Cybersource_Secret = "a0c3f213eb800ed0476dbe3118700dd03cfc1e25818d96cc2d30b37866804412";
        public JudoEnvironment JudoEnvironment = JudoEnvironment.Sandbox;
    }
}
