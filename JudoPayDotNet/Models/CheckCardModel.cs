using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Data to check a card (pre-auth with zero amount)
    /// </summary>
    [DataContract]
    // ReSharper disable UnusedMember.Global
    public class CheckCardModel : RegisterCardModel
    {
    }
    // ReSharper restore UnusedMember.Global
}
