using System;
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

        [LocalizedDescription("Sale")]
        PAYMENT = 1,

        [LocalizedDescription("Refund")]
        REFUND = 2,

        [LocalizedDescription("PreAuth")]
        PREAUTH = 3,

        [LocalizedDescription("Collection")]
        COLLECTION = 5,
    }
    // ReSharper restore InconsistentNaming
    // ReSharper restore UnusedMember.Global
}