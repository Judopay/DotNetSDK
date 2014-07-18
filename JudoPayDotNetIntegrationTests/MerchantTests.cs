using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class MerchantsTests
    {
        [Test]
        public void GetMerchantByJudoId()
        {
            var judo = JudoPaymentsFactory.Create("v4vBD2aOTj41wIYj",
                "d46b106358e675001f9d655efa9582f7d338d483ba24695d67f212d7c68bfd08",
                Configuration.BASEADDRESS);

            var merchant = judo.Merchants.Get("100016").Result;

            Assert.IsNotNull(merchant);
            Assert.IsFalse(merchant.HasError);
            Assert.IsNotNull(merchant.Response);
        }
    }
}
