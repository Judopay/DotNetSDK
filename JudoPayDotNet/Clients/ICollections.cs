using System.Threading.Tasks;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Clients
{
    public interface ICollections
    {
        Task<IResult<PaymentReceiptModel>> Create(CollectionModel collection);
    }
}
