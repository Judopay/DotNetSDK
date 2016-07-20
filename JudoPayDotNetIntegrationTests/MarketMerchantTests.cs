using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    internal class MarketMerchantTests : IntegrationTestsBase
    {

        [Test]
        public void GetAllMerchants()
        {
            var merchants = JudoPayApiElevated.Market.Merchants.Get().Result;
            Assert.IsNotNull(merchants);
            Assert.IsFalse(merchants.HasError);
            Assert.IsNotNull(merchants.Response);
        }
    }
}
