using JudoPayDotNet.Enums;

namespace JudoPayDotNetIntegrationTests
{
    public class Configuration
    {
        public string WebpaymentsUrl = "https://pay.judopay-sandbox.com/v1/Pay";

        public string Judoid = "{BASE_JUDOID}";
        public string Token = "{BASE_TOKEN}";
        public string Secret = "{BASE_SECRET}";
        public string ElevatedPrivilegesToken = "{BASE_TOKEN_ELEVATED}";
        public string ElevatedPrivilegesSecret = "{BASE_SECRET_ELEVATED}";


        public string Cybersource_Judoid = "{CYB_JUDOID}";
        public string Cybersource_Token = "{CYB_TOKEN}";
        public string Cybersource_Secret = "{CYB_SECRET}";

        public string SafeCharge_Judoid = "{SC_JUDOID}";
        public string SafeCharge_Token = "{SC_TOKEN}";
        public string SafeCharge_Secret = "{SC_SECRET}";

        public JudoEnvironment JudoEnvironment = JudoEnvironment.Sandbox;
    }
}