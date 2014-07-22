using JudoPayDotNet.Clients.WebPayments;

namespace JudoPayDotNet
{
    public interface IWebPayments
    {
        IPayments Payments { get; set; }
        IPreAuths PreAuths { get; set; }
        ITransactions Transactions { get; set; }
    }
}