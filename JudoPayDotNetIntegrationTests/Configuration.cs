using JudoPayDotNet.Enums;

namespace JudoPayDotNetIntegrationTests
{
    public class Configuration
    {
        public string WebpaymentsUrl = "https://pay.judopay-sandbox.com/v1/Pay";

        public string Judoid = "_BASE_JUDOID";
        public string Token = "_BASE_TOKEN";
        public string Secret = "_BASE_SECRET";
        public string ElevatedPrivilegesToken = "_BASE_TOKEN_ELEVATED";
        public string ElevatedPrivilegesSecret = "_BASE_SECRET_ELEVATED";


        public string Cybersource_Judoid = "_CYB_JUDOID";
        public string Cybersource_Token = "_CYB_TOKEN";
        public string Cybersource_Secret = "_CYB_SECRET";

        public string SafeCharge_Judoid = "_SC_JUDOID";
        public string SafeCharge_Token = "_SC_TOKEN";
        public string SafeCharge_Secret = "_SC_SECRET";

        public JudoEnvironment JudoEnvironment = JudoEnvironment.Sandbox;
    }
}