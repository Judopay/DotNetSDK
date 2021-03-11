using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class CheckCardTests : IntegrationTestsBase
    {
        [Test]
        public async Task CheckCard()
        {
            var checkCardModel = GetCheckCardModel(Configuration.Cybersource_Judoid);

            var response = await JudoPayApiCyberSource.CheckCards.Create(checkCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var receipt = response.Response as PaymentReceiptModel;
            Assert.NotNull(receipt);
            Assert.AreEqual("CheckCard", receipt.Type);
        }

        [Test]
        public void CheckEncryptedCard()
        {
            var checkEncryptedCardModel = GetCheckEncryptedCardModel(Configuration.Cybersource_Judoid).Result;

            var response = JudoPayApiCyberSource.CheckCards.Create(checkEncryptedCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public async Task CheckCardAndATokenPayment()
        {
            var checkCardModel = GetCheckCardModel(Configuration.Cybersource_Judoid);

            var response = await JudoPayApiCyberSource.CheckCards.Create(checkCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;
            var consumerReference = receipt.Consumer.YourConsumerReference;
            var paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 27, judoId: Configuration.Cybersource_Judoid);

            response = await JudoPayApiCyberSource.Payments.Create(paymentWithToken);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test, TestCaseSource(typeof(RegisterCheckCardTestSource), nameof(RegisterCheckCardTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(CheckCardModel checkCardModel, JudoModelErrorCode expectedModelErrorCode)
        {
            var checkCardReceiptResult = JudoPayApiIridium.CheckCards.Create(checkCardModel).Result;
            Assert.NotNull(checkCardReceiptResult);
            Assert.IsTrue(checkCardReceiptResult.HasError);
            Assert.IsNull(checkCardReceiptResult.Response);
            Assert.IsNotNull(checkCardReceiptResult.Error);
            Assert.AreEqual((int)JudoApiError.General_Model_Error, checkCardReceiptResult.Error.Code);

            var fieldErrors = checkCardReceiptResult.Error.ModelErrors;
            Assert.IsNotNull(fieldErrors);
            Assert.IsTrue(fieldErrors.Count >= 1);
            Assert.IsTrue(fieldErrors.Any(x => x.Code == (int)expectedModelErrorCode));
        }

        internal class RegisterCheckCardTestSource
        {
            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new CheckCardModel
                    {
                        CardNumber = "4976000000003436",
                        CV2 = "452",
                        ExpiryDate = "12/25",
                        YourConsumerReference = null,
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Consumer_Reference_Not_Supplied_1).SetName("ValidateRegisterCheckCardMissingConsumerReference");
                    yield return new TestCaseData(new CheckCardModel
                    {
                        CardNumber = "4976000000003436",
                        CV2 = "452",
                        ExpiryDate = "12/25",
                        YourConsumerReference = "",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Consumer_Reference_Length_2).SetName("ValidateRegisterCheckCardEmptyConsumerReference");
                    yield return new TestCaseData(new CheckCardModel
                    {
                        CardNumber = "4976000000003436",
                        CV2 = "452",
                        ExpiryDate = "12/25",
                        YourConsumerReference = "123456789012345678901234567890123456789012345678901",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Consumer_Reference_Length_2).SetName("ValidateRegisterCheckCardConsumerReferenceTooLong");
                    yield return new TestCaseData(new CheckCardModel
                    {
                        CardNumber = null,
                        CV2 = "452",
                        ExpiryDate = "12/25",
                        YourConsumerReference = "UniqueRef",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Card_Number_Not_Supplied).SetName("ValidateRegisterCheckCardMissingCardNumber");
                    yield return new TestCaseData(new CheckCardModel
                    {
                        CardNumber = "",
                        CV2 = "452",
                        ExpiryDate = "12/25",
                        YourConsumerReference = "UniqueRef",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Card_Number_Not_Supplied).SetName("ValidateRegisterCheckCardEmptyCardNumber");
                    yield return new TestCaseData(new CheckCardModel
                    {
                        CardNumber = "4976000000003436",
                        CV2 = "452",
                        ExpiryDate = null,
                        YourConsumerReference = "UniqueRef",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Expiry_Date_Not_Supplied).SetName("ValidateRegisterCheckCardMissingExpiryDate");
                    yield return new TestCaseData(new CheckCardModel
                    {
                        CardNumber = "4976000000003436",
                        CV2 = "452",
                        ExpiryDate = "",
                        YourConsumerReference = "UniqueRef",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Expiry_Date_Not_Supplied).SetName("ValidateRegisterCheckCardEmptyExpiryDate");

                    yield return new TestCaseData(new CheckEncryptedCardModel
                    {
                        OneUseToken = "DummyOneUseToken",
                        YourConsumerReference = null,
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Consumer_Reference_Not_Supplied_1).SetName("ValidateRegisterCheckEncryptedCardMissingConsumerReference");
                    yield return new TestCaseData(new CheckEncryptedCardModel
                    {
                        OneUseToken = "DummyOneUseToken",
                        YourConsumerReference = "",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Consumer_Reference_Length_2).SetName("ValidateRegisterCheckEncryptedCardEmptyConsumerReference");
                    yield return new TestCaseData(new CheckEncryptedCardModel
                    {
                        OneUseToken = "DummyOneUseToken",
                        YourConsumerReference = "123456789012345678901234567890123456789012345678901",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Consumer_Reference_Length_2).SetName("ValidateRegisterCheckEncryptedCardConsumerReferenceTooLong");
                    yield return new TestCaseData(new CheckEncryptedCardModel
                    {
                        OneUseToken = null,
                        YourConsumerReference = "UniqueRef",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.EncryptedBlobNotSupplied).SetName("ValidateRegisterCheckEncryptedCardMissingOneUseToken");
                    yield return new TestCaseData(new CheckEncryptedCardModel
                    {
                        OneUseToken = "",
                        YourConsumerReference = "UniqueRef",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.EncryptedBlobNotSupplied).SetName("ValidateRegisterCheckEncryptedCardEmptyOneUseToken");
                }
            }
        }

    }
}
