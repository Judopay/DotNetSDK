using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// The card transaction types supported by our gateway
    /// </summary>
    // ReSharper disable UnusedMember.Global
    // ReSharper disable InconsistentNaming
    public enum TransactionType : long
    {
        UNKNOWN = 0,

        [Description("payments")]
        [LocalizedDescription("Sale")]
        PAYMENT = 1,

        [Description("refunds")]
        [LocalizedDescription("Refund")]
        REFUND = 2,

        [Description("preauths")]
        [LocalizedDescription("PreAuth")]
        PREAUTH = 3,

        [Description("collections")]
        [LocalizedDescription("Collection")]
        COLLECTION = 5,
    }
    // ReSharper restore InconsistentNaming
    // ReSharper restore UnusedMember.Global
}