using System.Collections;
using System.Linq;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class RefundsTests : IntegrationTestsBase
    {
        [Test]
        public void ASimplePaymentAndRefund()
        {
            var paymentWithCard = GetCardPaymentModel();
            var response = JudoPayApiIridium.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var refund = new RefundModel
            {
                Amount = 25,
                ReceiptId = response.Response.ReceiptId,
               
            };

            response = JudoPayApiIridium.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);
        }

        [Test]
        public void APreAuthTwoCollectionsAndTwoRefunds()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var preAuthResponse = JudoPayApiIridium.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(preAuthResponse);
            Assert.IsFalse(preAuthResponse.HasError);
            Assert.AreEqual("Success", preAuthResponse.Response.Result);

            var collection = new CollectionModel
            {
                Amount = 24,
                ReceiptId = preAuthResponse.Response.ReceiptId,
                
            };

            var collection1Response = JudoPayApiIridium.Collections.Create(collection).Result;

            Assert.IsNotNull(collection1Response);
            Assert.IsFalse(collection1Response.HasError);

            var receipt = collection1Response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);

            collection = new CollectionModel
            {
                Amount = 1,
                ReceiptId = preAuthResponse.Response.ReceiptId,
                
            };

            var collection2Response = JudoPayApiIridium.Collections.Create(collection).Result;

            Assert.IsNotNull(collection2Response);
            Assert.IsFalse(collection2Response.HasError);

            receipt = collection2Response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);

            var refund = new RefundModel
            {
                Amount = 24,
                ReceiptId = collection1Response.Response.ReceiptId,
                
            };

            var response = JudoPayApiIridium.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);

            refund = new RefundModel
            {
                Amount = 1,
                ReceiptId = collection2Response.Response.ReceiptId,
                
            };

            response = JudoPayApiIridium.Refunds.Create(refund).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Refund", receipt.Type);
        }

        [Test, TestCaseSource(typeof(RefundsTestSource), nameof(RefundsTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(RefundModel refundModel, JudoModelErrorCode expectedModelErrorCode)
        {
            var collectionReceiptResult = JudoPayApiIridium.Refunds.Create(refundModel).Result;

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

        internal class RefundsTestSource
        {
            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new RefundModel
                    {
                        Amount = 1.20m
                    }, JudoModelErrorCode.ReceiptId_Is_Invalid).SetName("ValidateRefundMissingReceiptId"); // No ReceiptId_Not_Supplied as ReceiptId will be set to 0 as it is not null
                    yield return new TestCaseData(new RefundModel
                    {
                        ReceiptId = -1,
                        Amount = 1.20m
                    }, JudoModelErrorCode.ReceiptId_Is_Invalid).SetName("ValidateRefundInvalidReceiptId"); ;
                    yield return new TestCaseData(new RefundModel
                    {
                        ReceiptId = 685187481842388992,
                        Amount = 0m
                    }, JudoModelErrorCode.Amount_Greater_Than_0).SetName("ValidateRefundZeroAmount");
                    yield return new TestCaseData(new RefundModel
                    {
                        ReceiptId = 685187481842388992,
                        Amount = -1m
                    }, JudoModelErrorCode.Amount_Greater_Than_0).SetName("ValidateRefundNegativeAmount");
                }
            }
        }
    }
}
