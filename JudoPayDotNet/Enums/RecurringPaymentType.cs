namespace JudoPayDotNet.Enums
{
    public enum RecurringPaymentType : long
    {
        [LocalizedDescription("Unknown")]
        Unknown = 0,

        /// <summary>
        /// Scheduled regular payment
        /// </summary>
        [LocalizedDescription("RECURRING")]
        Recurring = 1,

        /// <summary>
        /// Unscheduled payment
        /// </summary>
        [LocalizedDescription("MIT")]
        Mit = 2
    }
}