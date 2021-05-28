namespace JudoPayDotNet.Enums
{
    public enum ThreeDSecureTwoScaExemption : long
    {
        /// <summary>
        /// Low Value
        /// </summary>
        [LocalizedDescription("LowValue ")]
        LowValue = 0,

        /// <summary>
        /// Secure Corporate
        /// </summary>
        [LocalizedDescription("SecureCorporate")]
        SecureCorporate = 1,

        /// <summary>
        /// Trusted Beneficiary
        /// </summary>
        [LocalizedDescription("TrustedBeneficiary")]
        TrustedBeneficiary = 2,

        /// <summary>
        /// Transaction Risk Analysis
        /// </summary>
        [LocalizedDescription("TransactionRiskAnalysis")]
        TransactionRiskAnalysis = 3
    }
}