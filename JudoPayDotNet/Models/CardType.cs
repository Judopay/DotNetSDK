using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Card types
    /// </summary>
    /// <remarks>This enum is fed from two data sources, so I'm abusing the LocalizedDescription attribute to do it - BJK</remarks>
    public enum CardType
    {
        [Description("UNKNOWN")]
        UNKNOWN = 0,

        [LocalizedDescription("VISA CREDIT")]
        [Description("VISA")]
        VISA = 1,

        [LocalizedDescription("MCI CREDIT")]
        [Description("MASTERCARD")]
        MASTERCARD = 2,

        [LocalizedDescription("ELECTRON")]
        [Description("VISA_ELECTRON")]
        VISA_ELECTRON = 3,

        [Description("SWITCH")]
        SWITCH = 4,

        [Description("SOLO")]
        SOLO = 5,

        [Description("LASER")]
        LASER = 6,

        [Description("CHINA_UNION_PAY")]
        CHINA_UNION_PAY = 7,

        [Description("AMEX")]
        AMEX = 8,

        [Description("JCB")]
        JCB = 9,

        [Description("MAESTRO")]
        MAESTRO = 10,

        [LocalizedDescription("VISA DEBIT")]
        [Description("VISA_DEBIT")]
        VISA_DEBIT = 11,

         //<summary>
         //In the europe mastercard debit is MAESTRO, however there maybe international cards
         //</summary>
        [LocalizedDescription("MCI DEBIT")]
        MASTERCARD_DEBIT = 12,

        [LocalizedDescription("VISA_PURCHASING")]
        VISA_PURCHASING = 13
    }
}
