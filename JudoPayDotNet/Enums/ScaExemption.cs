namespace JudoPayDotNet.Enums
{
    public enum ThreeDSecureTwoScaExemption : long
    {
        /// <summary>
        /// No value will be set
        /// </summary>
        [LocalizedDescription("Unknown")]
        Unknown = 0,

        /// <summary>
        /// Low Value
        /// </summary>
        [LocalizedDescription("LowValue ")]
        LowValue = 1,

        /// <summary>
        /// Secure Corporate
        /// </summary>
        [LocalizedDescription("SecureCorporate")]
        SecureCorporate = 2,

        /// <summary>
        /// Trusted Beneficiary
        /// </summary>
        [LocalizedDescription("TrustedBeneficiary")]
        TrustedBeneficiary = 3,

        /// <summary>
        /// Transaction Risk Analysis
        /// </summary>
        [LocalizedDescription("TransactionRiskAnalysis")]
        TransactionRiskAnalysis = 4
    }
}