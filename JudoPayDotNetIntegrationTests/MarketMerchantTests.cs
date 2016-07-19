using System.Net;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    internal class MarketMerchantTests
    {

        private readonly Configuration _configuration = new Configuration();

        [SetUp]
        public void SetUp()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
        }

        [Test]
        public void GetAllMerchants()
        {
            var judo = JudoPaymentsFactory.Create(_configuration.JudoEnvironment, _configuration.ElevatedPrivilegesToken, _configuration.ElevatedPrivilegesSecret);
            
            var merchants = judo.Market.Merchants.Get().Result;

            Assert.IsNotNull(merchants);
            Assert.IsFalse(merchants.HasError);
            Assert.IsNotNull(merchants.Response);
        }
    }
}
