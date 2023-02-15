using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class SaveCardTest : IntegrationTestsBase
    {
        [Test]
        public void SaveCard()
        {
            var saveCardModel = GetSaveCardModel("432438862");

            var response = JudoPayApiIridium.SaveCards.Create(saveCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void SaveEncryptedCard()
        {
            var saveEncryptedCardModel = GetSaveEncryptedCardModel().Result;

            var response = JudoPayApiIridium.SaveCards.Create(saveEncryptedCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void SaveCardRequiresCv2()
        {
            var saveCardModel = GetSaveCardModel("432438862", "4976000000003436", null);

            var response = JudoPayApiIridium.SaveCards.Create(saveCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasError);
        }

        [Test]
        public void SaveCardAndATokenPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();

            var saveCardModel = GetSaveCardModel(consumerReference);

            var response = JudoPayApiIridium.SaveCards.Create(saveCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;

            var paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 26);

            response = JudoPayApiIridium.Payments.Create(paymentWithToken).Result;

            paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 27);

            response = JudoPayApiIridium.Payments.Create(paymentWithToken).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test, TestCaseSource(typeof(SaveCardTestSource), nameof(SaveCardTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(SaveCardModel saveCardModel, JudoModelErrorCode expectedModelErrorCode)
        {
            var saveCardReceiptResult = JudoPayApiIridium.SaveCards.Create(saveCardModel).Result;
            Assert.NotNull(saveCardReceiptResult);
            Assert.IsTrue(saveCardReceiptResult.HasError);
            Assert.IsNull(saveCardReceiptResult.Response);
            Assert.IsNotNull(saveCardReceiptResult.Error);
            Assert.AreEqual((int)JudoApiError.General_Model_Error, saveCardReceiptResult.Error.Code);

            var fieldErrors = saveCardReceiptResult.Error.ModelErrors;
            Assert.IsNotNull(fieldErrors);
            Assert.IsTrue(fieldErrors.Count >= 1);
            Assert.IsTrue(fieldErrors.Any(x => x.Code == (int)expectedModelErrorCode));
        }

        [Test]
        public async Task TestDocs()
        {
            var client = JudoPayApiThreeDSecure2;

            //Create an instance of the SaveCardModel
            var saveCardRequest = new SaveCardModel()
            {
                JudoId = "100915867",
                YourConsumerReference = "yourConsumerReference",
                CardNumber = "4976000000003436",
                ExpiryDate = "12/25"
            };

            //Send the request to Judopay
            var response = await client.SaveCards.Create(saveCardRequest);

            if (response.HasError)
            {
                if (response.Error.Code == (int)HttpStatusCode.Forbidden)
                {
                    // Failed to authenticate - check your credentials
                }
                else if (response.Error.ModelErrors != null)
                {
                    // Validation failed on the request, check each list entry for details
                }
                else
                {
                    // Refer to https://docs.judopay.com/Content/Developer%20Tools/Codes.htm#Errors
                    var errorCode = response.Error.Code;
                }
            }
            else if (response.Response is PaymentReceiptModel receipt)
            {
                var receiptId = receipt.ReceiptId;
                var status = receipt.Result;
                if (receipt.Result == "Success")
                {
                    var cardToken = receipt.CardDetails.CardToken;
                }
            }
        }

        internal class SaveCardTestSource
        {
            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new SaveCardModel
                    {
                        CardNumber = "4976000000003436",
                        ExpiryDate = "12/25",
                        YourConsumerReference = null
                    }, JudoModelErrorCode.Consumer_Reference_Not_Supplied_1).SetName("ValidateSaveCardMissingConsumerReference");
                    yield return new TestCaseData(new SaveCardModel
                    {
                        CardNumber = "4976000000003436",
                        ExpiryDate = "12/25",
                        YourConsumerReference = ""
                    }, JudoModelErrorCode.Consumer_Reference_Length_2).SetName("ValidateSaveCardEmptyConsumerReference");
                    yield return new TestCaseData(new SaveCardModel
                    {
                        CardNumber = "4976000000003436",
                        ExpiryDate = "12/25",
                        YourConsumerReference = "123456789012345678901234567890123456789012345678901"
                    }, JudoModelErrorCode.Consumer_Reference_Length_2).SetName("ValidateSaveCardConsumerReferenceTooLong");
                    yield return new TestCaseData(new SaveCardModel
                    {
                        CardNumber = null,
                        ExpiryDate = "12/25",
                        YourConsumerReference = "UniqueRef"
                    }, JudoModelErrorCode.Card_Number_Not_Supplied).SetName("ValidateSaveCardMissingCardNumber");
                    yield return new TestCaseData(new SaveCardModel
                    {
                        CardNumber = "",
                        ExpiryDate = "12/25",
                        YourConsumerReference = "UniqueRef"
                    }, JudoModelErrorCode.Card_Number_Not_Supplied).SetName("ValidateSaveCardEmptyCardNumber");
                    yield return new TestCaseData(new SaveCardModel
                    {
                        CardNumber = "4976000000003436",
                        ExpiryDate = null,
                        YourConsumerReference = "UniqueRef"
                    }, JudoModelErrorCode.Expiry_Date_Not_Supplied).SetName("ValidateSaveCardMissingExpiryDate");
                    yield return new TestCaseData(new SaveCardModel
                    {
                        CardNumber = "4976000000003436",
                        ExpiryDate = "",
                        YourConsumerReference = "UniqueRef"
                    }, JudoModelErrorCode.Expiry_Date_Not_Supplied).SetName("ValidateSaveCardEmptyExpiryDate");
                    yield return new TestCaseData(new SaveEncryptedCardModel
                    {
                        OneUseToken = "DummyOneUseToken",
                        YourConsumerReference = null,
                    }, JudoModelErrorCode.Consumer_Reference_Not_Supplied_1).SetName("ValidateSaveEncryptedCardMissingConsumerReference");
                    yield return new TestCaseData(new SaveEncryptedCardModel
                    {
                        OneUseToken = "DummyOneUseToken",
                        YourConsumerReference = "",
                    }, JudoModelErrorCode.Consumer_Reference_Length_2).SetName("ValidateSaveEncryptedCardEmptyConsumerReference");
                    yield return new TestCaseData(new SaveEncryptedCardModel
                    {
                        OneUseToken = "DummyOneUseToken",
                        YourConsumerReference = "123456789012345678901234567890123456789012345678901"
                    }, JudoModelErrorCode.Consumer_Reference_Length_2).SetName("ValidateSaveEncryptedCardConsumerReferenceTooLong");
                    yield return new TestCaseData(new SaveEncryptedCardModel
                    {
                        OneUseToken = null,
                        YourConsumerReference = "UniqueRef",
                    }, JudoModelErrorCode.EncryptedBlobNotSupplied).SetName("ValidateSaveEncryptedCardMissingOneUseToken");
                    yield return new TestCaseData(new SaveEncryptedCardModel
                    {
                        OneUseToken = "",
                        YourConsumerReference = "UniqueRef",
                    }, JudoModelErrorCode.EncryptedBlobNotSupplied).SetName("ValidateSaveEncryptedCardEmptyOneUseToken");
                }
            }
        }

    }
}
