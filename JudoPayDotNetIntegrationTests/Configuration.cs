using JudoPayDotNet.Enums;

namespace JudoPayDotNetIntegrationTests
{
    public class Configuration
    {
        public string WebpaymentsUrl = "https://pay.judopay-sandbox.com/v1/Pay";

        public string Judoid = "_baseJudoId";
        public string Token = "_baseToken";
        public string Secret = "_baseSecret";
        public string ElevatedPrivilegesSecret = "_baseElevatedToken";
        public string ElevatedPrivilegesToken = "_baseElevatedSecret";


        public string Cybersource_Judoid = "_cybJudoId";
        public string Cybersource_Token = "_cybToken";
        public string Cybersource_Secret = "_cybSecret";

        public string SafeCharge_Judoid = "_scJudoId";
        public string SafeCharge_Token = "_scToken";
        public string SafeCharge_Secret = "_scSecret";

        public JudoEnvironment JudoEnvironment = JudoEnvironment.Sandbox;
    }
}