using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetTests.Enums
{
    [TestFixture]
    public class ExplanationAttributeTests
    {
        [Test]
        public void GetExplanation()
        {
            var type = PaymentErrorCodesPublic.PaymentDeclined;

            var explanation = EnumUtils.GetEnumExplanation(type);

            Assert.AreEqual("The payment has been declined by the card's issuing bank.", explanation);
        }
    }
}