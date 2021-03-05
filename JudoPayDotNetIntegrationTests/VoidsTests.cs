using System.Collections;
using System.Linq;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class VoidsTests : IntegrationTestsBase
    {
        [Test, TestCaseSource(typeof(VoidsTestSource), nameof(VoidsTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(VoidModel voidModel, JudoModelErrorCode expectedModelErrorCode)
        {
            var voidReceiptResult = JudoPayApiIridium.Voids.Create(voidModel).Result;

            Assert.NotNull(voidReceiptResult);
            Assert.IsTrue(voidReceiptResult.HasError);
            Assert.IsNull(voidReceiptResult.Response);
            Assert.IsNotNull(voidReceiptResult.Error);
            Assert.AreEqual((int)JudoApiError.General_Model_Error, voidReceiptResult.Error.Code);

            var fieldErrors = voidReceiptResult.Error.ModelErrors;
            Assert.IsNotNull(fieldErrors);
            Assert.IsTrue(fieldErrors.Count >= 1);
            Assert.IsTrue(fieldErrors.Any(x => x.Code == (int)expectedModelErrorCode));
        }

        internal class VoidsTestSource
        {
            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new VoidModel
                    {
                        Amount = 1.20m
                    }, JudoModelErrorCode.ReceiptId_Is_Invalid).SetName("ValidateVoidMissingReceiptId"); // No ReceiptId_Not_Supplied as ReceiptId will be set to 0 as it is not null
                    yield return new TestCaseData(new VoidModel
                    {
                        ReceiptId = -1,
                        Amount = 1.20m
                    }, JudoModelErrorCode.ReceiptId_Is_Invalid).SetName("ValidateVoidInvalidReceiptId");
                    yield return new TestCaseData(new VoidModel
                    {
                        ReceiptId = 685187481842388992
                    }, JudoModelErrorCode.Amount_Not_Valid).SetName("ValidateVoidMissingAmount");
                    yield return new TestCaseData(new VoidModel
                    {
                        ReceiptId = 685187481842388992,
                        Amount = 0m
                    }, JudoModelErrorCode.Amount_Not_Valid).SetName("ValidateVoidInvalidAmount");
                }
            }
        }
    }
}
