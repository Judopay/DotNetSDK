using System.Runtime.Serialization;

namespace JudoPayDotNet.Models
{
    /// <summary>
    /// Data to check a card
    /// </summary>
    [DataContract]
    [KnownType(typeof(CheckEncryptedCardModel))]
    // ReSharper disable UnusedMember.Global
    public class CheckCardModel : RegisterCardModel
    {
    }
    // ReSharper restore UnusedMember.Global
}
