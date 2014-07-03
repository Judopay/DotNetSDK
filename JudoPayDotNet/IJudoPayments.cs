namespace JudoPayDotNet
{
    public interface IJudoPayments
    {
        IPayments Payments { get; set; }
        IRefunds Refunds { get; set; }
        IPreAuths PreAuths { get; set; }
        ITransactions Transactions { get; set; }
    }
}