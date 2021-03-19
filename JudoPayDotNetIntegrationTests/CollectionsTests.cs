using System.Collections;
using System.Linq;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class CollectionsTests : IntegrationTestsBase
    {
        [Test, TestCaseSource(typeof(CollectionsTestSource), nameof(CollectionsTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(CollectionModel collectionModel, JudoModelErrorCode expectedModelErrorCode)
        {
            var collectionReceiptResult = JudoPayApiIridium.Collections.Create(collectionModel).Result;

            Assert.NotNull(collectionReceiptResult);
            Assert.IsTrue(collectionReceiptResult.HasError);
            Assert.IsNull(collectionReceiptResult.Response);
            Assert.IsNotNull(collectionReceiptResult.Error);
            Assert.AreEqual((int)JudoApiError.General_Model_Error, collectionReceiptResult.Error.Code);

            var fieldErrors = collectionReceiptResult.Error.ModelErrors;
            Assert.IsNotNull(fieldErrors);
            Assert.IsTrue(fieldErrors.Count >= 1);
            Assert.IsTrue(fieldErrors.Any(x => x.Code == (int)expectedModelErrorCode));
        }

        internal class CollectionsTestSource
        {
            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new CollectionModel
                    {
                        Amount = 1.20m
                    }, JudoModelErrorCode.ReceiptId_Is_Invalid).SetName("ValidateCollectionMissingReceiptId"); // No ReceiptId_Not_Supplied as ReceiptId will be set to 0 as it is not null
                    yield return new TestCaseData(new CollectionModel
                    {
                        ReceiptId = -1,
                        Amount = 1.20m
                    }, JudoModelErrorCode.ReceiptId_Is_Invalid).SetName("ValidateCollectionInvalidReceiptId");
                    // Change both following to expect Amount_Greater_Than_0 once 6.3 release deployed
                    yield return new TestCaseData(new CollectionModel
                    {
                        ReceiptId = 685187481842388992,
                        Amount = 0m
                    }, JudoModelErrorCode.Amount_Greater_Than_0).SetName("ValidateCollectionZeroAmount");
                    yield return new TestCaseData(new CollectionModel
                    {
                        ReceiptId = 685187481842388992,
                        Amount = -1m
                    }, JudoModelErrorCode.Amount_Greater_Than_0).SetName("ValidateCollectionNegativeAmount");
                }
            }
        }
    }
}
