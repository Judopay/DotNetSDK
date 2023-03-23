using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            // Given a standard SaveCard model
            var saveCardModel = GetSaveCardModel();

            // When SaveCards.Create is called
            var response = JudoPayApiBase.SaveCards.Create(saveCardModel).Result;

            // Then a successful PaymentReceiptModel is returned with a card token
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var saveCardReceipt = response.Response as PaymentReceiptModel;
            Assert.IsNotNull(saveCardReceipt);
            Assert.IsNotNull(saveCardReceipt.CardDetails.CardToken);
        }

        [Test]
        public void SaveCardTwiceWithSameDetailsReturnsSameToken()
        {
            // Given a standard SaveCard model with a specific consumer reference
            var consumerReference = Guid.NewGuid().ToString();
            var saveCardModel = GetSaveCardModel(consumerReference);

            // When SaveCards.Create is called
            var response = JudoPayApiBase.SaveCards.Create(saveCardModel).Result;

            // Then a successful PaymentReceiptModel is returned with a card token
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var saveCardReceipt = response.Response as PaymentReceiptModel;
            Assert.IsNotNull(saveCardReceipt);
            Assert.IsNotNull(saveCardReceipt.CardDetails.CardToken);

            var firstCardToken = saveCardReceipt.CardDetails.CardToken;

            // And if a second call is made to SaveCards.Create with the same card details and consumer reference
            var secondResponse = JudoPayApiBase.SaveCards.Create(saveCardModel).Result;
            Assert.IsNotNull(secondResponse);
            var secondSaveCardReceipt = secondResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(secondSaveCardReceipt);
            Assert.AreEqual(firstCardToken, secondSaveCardReceipt.CardDetails.CardToken);
        }

        [Test]
        public void SaveCardRequiresCv2()
        {
            // Given a SaveCard model with an address with no postCode set
            var saveCardModel = GetSaveCardModel(postCode: null);

            // When SaveCards.Create is called
            var response = JudoPayApiBase.SaveCards.Create(saveCardModel).Result;

            // Then an error is returned
            Assert.IsNotNull(response);
            Assert.IsTrue(response.HasError);

            var fieldErrors = response.Error.ModelErrors;
            Assert.IsNotNull(fieldErrors);
            Assert.IsTrue(fieldErrors.Count >= 1);
            Assert.IsTrue(fieldErrors.Any(x => x.Code == (int)JudoModelErrorCode.Postcode_Not_Supplied));
        }

        [Test]
        public void SaveCardAndATokenPayment()
        {
            // Given a standard SaveCard model with a specific consumer reference
            var consumerReference = Guid.NewGuid().ToString();
            var saveCardModel = GetSaveCardModel(consumerReference);

            // When SaveCards.Create is called
            var response = JudoPayApiBase.SaveCards.Create(saveCardModel).Result;

            // Then a successful receipt is returned with a card token
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;
            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;

            // And when a payment is used with the same cardToken and consumerReference
            var firstPaymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 26);
            // Then the payment is successful
            var firstPaymentResponse = JudoPayApiBase.Payments.Create(firstPaymentWithToken).Result;
            Assert.IsNotNull(firstPaymentResponse);
            Assert.IsFalse(firstPaymentResponse.HasError);
            Assert.AreEqual("Success", firstPaymentResponse.Response.Result);

            // And when a second payment is used with the same cardToken and consumerReference
            var secondPaymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 27);
            // Then that payment is also successful
            var secondPaymentResponse = JudoPayApiBase.Payments.Create(secondPaymentWithToken).Result;
            Assert.IsNotNull(secondPaymentResponse);
            Assert.IsFalse(secondPaymentResponse.HasError);
            Assert.AreEqual("Success", secondPaymentResponse.Response.Result);
        }

        [Test, TestCaseSource(typeof(SaveCardTestSource), nameof(SaveCardTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(SaveCardModel saveCardModel, JudoModelErrorCode expectedModelErrorCode)
        {
            var saveCardReceiptResult = JudoPayApiBase.SaveCards.Create(saveCardModel).Result;
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
            var consumerRef = Guid.NewGuid().ToString();
            var saveCardRequest = new SaveCardModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = consumerRef,
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
                    }, JudoModelErrorCode.Expiry_Date_Not_Supplied).SetName("ValidateSaveCardEmptyExpiryDate"); ;
                }
            }
        }

    }
}
