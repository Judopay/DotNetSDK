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
            var checkCardModel = GetCheckCardModel();

            var response = await JudoPayApiBase.CheckCards.Create(checkCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var receipt = response.Response as PaymentReceiptModel;
            Assert.NotNull(receipt);
            Assert.AreEqual("CheckCard", receipt.Type);
        }

        [Test]
        public async Task CheckCardAndATokenPayment()
        {
            var checkCardModel = GetCheckCardModel();

            var response = await JudoPayApiBase.CheckCards.Create(checkCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;
            var consumerReference = receipt.Consumer.YourConsumerReference;
            var paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 27);

            response = await JudoPayApiBase.Payments.Create(paymentWithToken);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test, TestCaseSource(typeof(RegisterCheckCardTestSource), nameof(RegisterCheckCardTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(CheckCardModel checkCardModel, JudoModelErrorCode expectedModelErrorCode)
        {
            var checkCardReceiptResult = JudoPayApiBase.CheckCards.Create(checkCardModel).Result;
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

        [Test]
        public async Task PrimaryAccountDetailsCheckCard()
        {
            var checkCardModel = GetCheckCardModel();
            // Given a RegisterCardModel with PrimaryAccountDetails
            checkCardModel.PrimaryAccountDetails = new PrimaryAccountDetailsModel
            {
                Name = "Doe",
                AccountNumber = "1234567",
                DateOfBirth = "2000-12-31",
                PostCode = "EC2A 4DP"
            };

            var response = await JudoPayApiBase.CheckCards.Create(checkCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public async Task ThreeDSecureMpiCheckCard()
        {
            var checkCardModel = GetCheckCardModel();
            // Given a CheckCardModel with ThreeDSecureMpiModel
            var threeDSecureMpi = new ThreeDSecureMpiModel
            {
                DsTransId = "539c3e09-ee16-404b-9eee-96941e9e012e",
                Cavv = "AAkBAgOViQAAAABkgmJXdAoPFww=",
                Eci = "05",
                ThreeDSecureVersion = "2.1.0"
            };
            checkCardModel.ThreeDSecureMpi = threeDSecureMpi;

            var response = await JudoPayApiBase.CheckCards.Create(checkCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;
            Assert.IsNotNull(receipt?.ThreeDSecure);
            Assert.AreEqual(threeDSecureMpi.Eci, receipt.ThreeDSecure.Eci);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public async Task DisableNetworkTokenisationReturned(bool disableNetworkTokenisation)
        {
            var checkCard = GetCheckCardModel();
            // Given a checkcard with the latest Api-Version (6.23+)
            checkCard.DisableNetworkTokenisation = disableNetworkTokenisation;
            var response = await JudoPayApiBase.CheckCards.Create(checkCard);

            Assert.IsNotNull(response);
            var receipt = response.Response as PaymentReceiptModel;
            Assert.IsNotNull(receipt);

            // And the DisableNetworkTokenisation attribute is populated as expected
            Assert.AreEqual(disableNetworkTokenisation, receipt.DisableNetworkTokenisation);

            // Also check historic receipts
            System.Threading.Thread.Sleep(1000);
            var historicReceiptResponse = JudoPayApiBase.Transactions.Get(response.Response.ReceiptId).Result;

            Assert.IsNotNull(historicReceiptResponse);
            Assert.IsFalse(historicReceiptResponse.HasError);

            var historicReceipt = historicReceiptResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(historicReceipt);
            Assert.AreEqual(disableNetworkTokenisation, historicReceipt.DisableNetworkTokenisation);
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
                    }, JudoModelErrorCode.Expiry_Date_Not_Valid).SetName("ValidateRegisterCheckCardEmptyExpiryDate");
                }
            }
        }

    }
}
