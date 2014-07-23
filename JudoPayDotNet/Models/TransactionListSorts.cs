using JudoPayDotNet.Enums;

namespace JudoPayDotNet.Models
{
    // ReSharper disable UnusedMember.Global
    // ReSharper disable InconsistentNaming
    public enum TransactionListSorts
    {

        Unknown = 0,

        [LocalizedDescription("time-descending")]

        timeDescending = 1,


        [LocalizedDescription("time-ascending")]
        timeAscending = 2
    }
    // ReSharper restore InconsistentNaming
    // ReSharper restore UnusedMember.Global
}
