using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Internal;
using NUnit.Framework;

namespace JudoPayDotNetTests.Clients
{
    [TestFixture]
    public class ThreeDsTests
    {
        [Test]
        public void TestInternalCompleteThreeDSecureTwoModel()
        {
            var cv2FromMerchant = "123";
            // Given an external model
            var externalComplete3Ds2Model = new CompleteThreeDSecureTwoModel
            {
                CV2 = cv2FromMerchant
            };

            // When that request is sent to PartnerApi
            var internalComplete3Ds2Model = InternalCompleteThreeDSecureTwoModel.From(externalComplete3Ds2Model);

            // Then version is always 2.0.0 and cv2 unchanged from external model
            Assert.AreEqual("2.0.0", internalComplete3Ds2Model.Version);
            Assert.AreEqual(cv2FromMerchant, internalComplete3Ds2Model.CV2);
        }
    }
}
