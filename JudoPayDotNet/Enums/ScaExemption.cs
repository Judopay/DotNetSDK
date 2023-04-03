namespace JudoPayDotNet.Enums
{
    public enum ThreeDSecureTwoScaExemption : long
    {
        // Use TransactionRiskAnalysis instead of LowValue
        //LowValue = 0,
        //SecureCorporate = 1,
        TrustedBeneficiary = 2,
        TransactionRiskAnalysis = 3,
        DataShareOnly = 4,
        ScaAlreadyPerformed = 5
    }
}