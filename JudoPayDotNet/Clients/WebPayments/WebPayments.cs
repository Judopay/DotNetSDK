namespace JudoPayDotNet.Clients.WebPayments
{
    internal class WebPayments : IWebPayments
    {
        public IPayments Payments { get; set; }

        public IPreAuths PreAuths { get; set; }

        public ITransactions Transactions { get; set; }
    }
}
