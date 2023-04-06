using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// A refund request
    /// </summary>
    /// <remarks>You can refund all or part of a collection or payment</remarks>
    [DataContract]
    public class RefundModel : ReferencingTransactionBase
    {
    }
}
