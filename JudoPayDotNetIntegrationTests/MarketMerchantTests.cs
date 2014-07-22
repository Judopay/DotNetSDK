using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    internal class MarketMerchantTests
    {
        [Test]
        public void GetAllMerchants()
        {
            var judo = JudoPaymentsFactory.Create("v4vBD2aOTj41wIYj",
                "d46b106358e675001f9d655efa9582f7d338d483ba24695d67f212d7c68bfd08",
                Configuration.Baseaddress);

            var merchants = judo.Market.Merchants.Get().Result;

            Assert.IsNotNull(merchants);
            Assert.IsFalse(merchants.HasError);
            Assert.IsNotNull(merchants.Response);
        }
    }
}
