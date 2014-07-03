using JudoPayDotNet.Models;

namespace JudoPayDotNet
{
    public interface IRefunds
    {
        void Create(RefundModel refund);
    }
}