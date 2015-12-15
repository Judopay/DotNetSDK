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
            var judo = JudoPaymentsFactory.Create(Configuration.ElevatedPrivilegesToken,
                Configuration.ElevatedPrivilegesSecret,
                Configuration.Baseaddress);

            var merchants = judo.Market.Merchants.Get().Result;

            Assert.IsNotNull(merchants);
            Assert.IsFalse(merchants.HasError);
            Assert.IsNotNull(merchants.Response);
        }
    }
}
