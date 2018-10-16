using System.Configuration;
using JudoPayDotNet.Enums;

namespace JudoPayDotNetIntegrationTests
{
    public class Configuration
    {
        public string WebpaymentsUrl = "https://pay.judopay-sandbox.com/v1/Pay";

        public string ElevatedPrivilegesSecret = "cc9274f05a6e558f4ed8be5ad0429f7edbb05a594d56f851c2b8a53fedfce3d9";
        public string ElevatedPrivilegesToken = "90GEiHrgjEuHnbAt";

        public string Iridium_Judoid = "100915867";
        public string Iridium_Token = "Izx9omsBR15LatAl";
        public string Iridium_Secret = "b5787124845533d8e68d12a586fa3713871b876b528600ebfdc037afec880cd6";

        public string Cybersource_Judoid = "100491943";
        public string Cybersource_Token = "4jLqa8a5yoAZCir8";
        public string Cybersource_Secret = "fce18777f015eee2f73abb7961b23320177994623fcc95643a7b39b8b155311f";

        public JudoEnvironment JudoEnvironment = JudoEnvironment.Sandbox;
    }
}
