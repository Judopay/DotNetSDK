using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Void a previously authorised (PreAuth) transaction
    /// </summary>
    [DataContract]
    public class VoidModel : ReferencingTransactionBase
    {
    }
}
