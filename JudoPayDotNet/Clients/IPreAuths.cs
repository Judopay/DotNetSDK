using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    public interface IPreAuths
    {
        ITransactionResult Create(CardPaymentModel cardPreAuth);
        ITransactionResult Create(TokenPaymentModel tokenPreAuth);
    }
}