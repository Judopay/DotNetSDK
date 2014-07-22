using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetTests.Enums
{
    [TestFixture]
    public class DescriptionAttributeTests
    {
        [Test]
        public void GetDescription()
        {
            const CardType type = CardType.AMEX;

            var description = EnumUtils.GetEnumDescription(type);

            Assert.AreEqual("AMEX", description);
        }

        [Test]
        public void GetValueFromDescription()
        {
            const string description = "AMEX";

            var type = EnumUtils.GetValueFromDescription<CardType>(description);

            Assert.AreEqual(CardType.AMEX, type);
        }
    }
}
