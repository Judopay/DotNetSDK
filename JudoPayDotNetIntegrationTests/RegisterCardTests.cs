using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class RegisterCardTest : IntegrationTestsBase
    {

        [Test]
        public async Task RegisterCard()
        {

            var registerCardModel = GetRegisterCardModel("432438862");

            var response = await JudoPayApiIridium.RegisterCards.Create(registerCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void RegisterEncryptedCard()
        {
            var registerEncryptedCardModel = GetRegisterEncryptedCardModel().Result;

            var response = JudoPayApiIridium.RegisterCards.Create(registerEncryptedCardModel).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public async Task RegisterCardAndATokenPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();

            var registerCard = GetRegisterCardModel(consumerReference);

            var response = await JudoPayApiIridium.RegisterCards.Create(registerCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;

            var paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 27);

            response = await JudoPayApiIridium.Payments.Create(paymentWithToken);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void ADeclinedCardPayment()
        {
            var registerCard = GetRegisterCardModel("432438862", "4221690000004963", "125");

            var response = JudoPayApiIridium.RegisterCards.Create(registerCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }

        [Test, TestCaseSource(typeof(CheckCardTests.RegisterCheckCardTestSource), nameof(CheckCardTests.RegisterCheckCardTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(RegisterCardModel registerCardModel, JudoModelErrorCode expectedModelErrorCode)
        {
            var registerCardReceiptResult = JudoPayApiIridium.RegisterCards.Create(registerCardModel).Result;
            Assert.NotNull(registerCardReceiptResult);
            Assert.IsTrue(registerCardReceiptResult.HasError);
            Assert.IsNull(registerCardReceiptResult.Response);
            Assert.IsNotNull(registerCardReceiptResult.Error);
            Assert.AreEqual((int)JudoApiError.General_Model_Error, registerCardReceiptResult.Error.Code);

            var fieldErrors = registerCardReceiptResult.Error.ModelErrors;
            Assert.IsNotNull(fieldErrors);
            Assert.IsTrue(fieldErrors.Count >= 1);
            Assert.IsTrue(fieldErrors.Any(x => x.Code == (int)expectedModelErrorCode));
        }

        [Test]
        public async Task PrimaryAccountDetailsRegisterCard()
        {
            var registerCardModel = GetRegisterCardModel("432438862");
            // Given a RegisterCardModel with PrimaryAccountDetails
            registerCardModel.PrimaryAccountDetails = new PrimaryAccountDetailsModel
            {
                Name = "Doe",
                AccountNumber = "1234567",
                DateOfBirth = "2000-12-31",
                PostCode = "EC2A 4DP"
            };

            var response = await JudoPayApiIridium.RegisterCards.Create(registerCardModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);
        }

        [Test]
        public void TestRegisterCardWithInvalidThreeDSecureMpi()
        {
            // Given a register model with an invalid ThreeDSecureMpiModel (one valid field)
            var registerModel = GetRegisterCardModel();
            registerModel.ThreeDSecureMpi = new ThreeDSecureMpiModel
            {
                Cavv = "",
                DsTransId = "",
                Eci = "",
                ThreeDSecureVersion = "2.1.0"
            };

            // When a payment is made with this model
            var result = JudoPayApiIridium.RegisterCards.Create(registerModel).Result;

            // Then PartnerApi will return fieldErrors for the invalid fields (proving that
            // the details are being passed on)
            Assert.NotNull(result.Error?.ModelErrors);
            Assert.AreEqual(3, result.Error.ModelErrors.Count);
            var firstFieldError = result.Error.ModelErrors.First();
            Assert.NotNull(firstFieldError);
            Assert.AreEqual(3250, firstFieldError.Code); // ThreeDSecure_Mpi_MissingFields
            Assert.AreEqual("ThreeDSecureMpi.Cavv", firstFieldError.FieldName);
            // Other errors are the same code for the other two invalid fields
        }
    }
}
