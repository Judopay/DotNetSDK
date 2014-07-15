using System;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// The card transaction types supported by our gateway
    /// </summary>
    public enum TransactionType : long
    {
        UNKNOWN = 0,

        [LocalizedDescription("Payment")]
        SALE = 1,

        [LocalizedDescription("Refund")]
        REFUND = 2,

        [LocalizedDescription("PreAuth")]
        PREAUTH = 3,

        VOID = 4,

        [LocalizedDescription("Collection")]
        COLLECTION = 5,

        [Obsolete("Iridium leftover from previous API versions")]
        RETRY = 6,
		
        /// <summary>
        /// This is a hang up from an older version of the Iridium API
        /// </summary>
        [Obsolete("Iridium leftover from previous API versions")]
        STORE = 7,

        [Obsolete("Iridium leftover from previous API versions")]
        KEEP_ALIVE = 8
    }
}