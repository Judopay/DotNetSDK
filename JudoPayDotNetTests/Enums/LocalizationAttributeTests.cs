using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetTests.Enums
{
    [TestFixture]
    public class LocalizationAttributeTests
    {
        [Test]
        public void GetDescription()
        {
            var type = CardType.MASTERCARD_DEBIT;

            var description = EnumUtils.GetEnumDescription(type);

            Assert.AreEqual("MCI DEBIT", description);
        }

        [Test]
        public void GetValueFromDescription()
        {
            var description = "MCI DEBIT";

            var type = EnumUtils.GetValueFromDescription<CardType>(description);

            Assert.AreEqual(CardType.MASTERCARD_DEBIT, type);
        }
    }
}