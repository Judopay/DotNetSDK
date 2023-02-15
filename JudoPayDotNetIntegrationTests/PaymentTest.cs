using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class PaymentTest : IntegrationTestsBase
    {
        [Test]
        public async Task ASimplePayment()
        {
            var paymentWithCard = GetCardPaymentModel();

            var response = await JudoPayApiIridium.Payments.Create(paymentWithCard);


            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

        }

        [Test]
        public async Task ARecurringPayment()
        {
            var initialPaymentWithCard = GetCardPaymentModel(initialRecurringPayment: true);

            var initialResponse = await JudoPayApiBase.Payments.Create(initialPaymentWithCard);

            Assert.IsNotNull(initialResponse);
            Assert.IsFalse(initialResponse.HasError);
            Assert.AreEqual("Success", initialResponse.Response.Result);

            var initialPaymentReceipt = initialResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(initialPaymentReceipt);
            Assert.IsNull(initialPaymentReceipt.Recurring);

            var recurringPaymentWithCard = GetCardPaymentModel(
                initialRecurringPayment: false, 
                recurringPayment: true,
                recurringPaymentType: RecurringPaymentType.Recurring, 
                relatedReceiptId: "" + initialPaymentReceipt.ReceiptId
            );

            var recurringResponse = await JudoPayApiBase.Payments.Create(recurringPaymentWithCard);
            Assert.IsNotNull(recurringResponse);
            Assert.IsFalse(recurringResponse.HasError);
            Assert.AreEqual("Success", recurringResponse.Response.Result);

            var recurringPaymentReceipt = recurringResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(recurringPaymentReceipt);
            Assert.AreEqual("RECURRING", recurringPaymentReceipt.RecurringPaymentType);
        }

        [Test]
        public async Task AnMitPayment()
        {
            var initialPaymentWithCard = GetCardPaymentModel(initialRecurringPayment: true);

            var initialResponse = await JudoPayApiBase.Payments.Create(initialPaymentWithCard);

            Assert.IsNotNull(initialResponse);
            Assert.IsFalse(initialResponse.HasError);
            Assert.AreEqual("Success", initialResponse.Response.Result);

            var initialPaymentReceipt = initialResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(initialPaymentReceipt);
            Assert.IsNull(initialPaymentReceipt.Recurring);

            var mitPaymentWithCard = GetCardPaymentModel(
                initialRecurringPayment: false, 
                recurringPayment: true,
                recurringPaymentType: RecurringPaymentType.Mit, 
                relatedReceiptId: "" + initialPaymentReceipt.ReceiptId
            );

            var mitResponse = await JudoPayApiBase.Payments.Create(mitPaymentWithCard);
            Assert.IsNotNull(mitResponse);
            Assert.IsFalse(mitResponse.HasError);
            Assert.AreEqual("Success", mitResponse.Response.Result);

            var mitPaymentReceipt = mitResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(mitPaymentReceipt);
            Assert.AreEqual("MIT", mitPaymentReceipt.RecurringPaymentType);
        }

        [Test]
        public async Task ATokenPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();
            var paymentWithCard = GetCardPaymentModel(consumerReference);

            var response = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;

            var paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 26);
            response = await JudoPayApiIridium.Payments.Create(paymentWithToken);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var cardTokenReceipt = response.Response as PaymentReceiptModel;
            Assert.That(cardTokenReceipt, Is.Not.Null);
            Assert.That(cardTokenReceipt.CardDetails, Is.Not.Null, "Missing carddetails property on receipt");
            Assert.That(cardTokenReceipt.CardDetails.CardLastfour, Is.Not.Empty);
            Assert.That(cardTokenReceipt.CardDetails.EndDate, Is.Not.Empty);
            Assert.That(cardTokenReceipt.CardDetails.CardScheme, Is.EqualTo("Visa"));
            Assert.That(cardTokenReceipt.CardDetails.CardFunding, Is.EqualTo("Debit"));
            // IssuerService no longer called for token transactions and Category not saved in DB
            Assert.That(cardTokenReceipt.CardDetails.CardCategory, Is.EqualTo(""));
            Assert.That(cardTokenReceipt.CardDetails.CardCountry, Is.EqualTo("FR"));
            Assert.That(cardTokenReceipt.CardDetails.Bank, Is.EqualTo("Credit Industriel Et Commercial"));
            Assert.That(cardTokenReceipt.Message, Does.Match("AuthCode: \\d{6}"), $"Result message on receipt not in correct format AuthCode: xxxxxx. Was {cardTokenReceipt.Message}");
            Assert.That(cardTokenReceipt.MerchantName, Is.Not.Empty);
            Assert.That(cardTokenReceipt.AppearsOnStatementAs, Is.Not.Empty);
            Assert.That(cardTokenReceipt.CreatedAt, Is.Not.Null);
            Assert.That(cardTokenReceipt.NetAmount, Is.GreaterThan(0));
            Assert.That(cardTokenReceipt.Amount, Is.GreaterThan(0));
            Assert.That(cardTokenReceipt.Currency, Is.EqualTo("GBP"));

            Assert.That(receipt.Risks.PostcodeCheck, Is.EqualTo("PASSED"));
            Assert.That(receipt.Risks.MerchantSuggestion, Is.EqualTo("Allow"));
        }

        [Test]
        public async Task ATokenRecurringPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();

            var paymentWithCard = GetCardPaymentModel(
                consumerReference, 
                recurringPayment: true,
                recurringPaymentType: RecurringPaymentType.Recurring
            );

            var response = await JudoPayApiBase.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            var paymentReceipt = response.Response as PaymentReceiptModel;

            Assert.IsInstanceOf<PaymentReceiptModel>(response.Response);
            Assert.AreEqual("RECURRING", paymentReceipt?.RecurringPaymentType);
        }

        [Test]
        public async Task AOneTimePayment()
        {
            var oneTimePaymentModel = GetOneTimePaymentModel().Result;

            var response = await JudoPayApiIridium.Payments.Create(oneTimePaymentModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);
        }

        [Test]
        public async Task ADeclinedCardPayment()
        {
            var paymentWithCard = GetCardPaymentModel("432438862", "4221690000004963", "125");

            var response = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }

        [Test]
        public async Task DeDuplicationTest()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response1 = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            var response2 = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.AreEqual(response1.Response.ReceiptId, response2.Response.ReceiptId);
        }
        
        [Test]
        public async Task PrimaryAccountDetailsPayment()
        {
            var paymentWithCard = GetPrimaryAccountPaymentModel();

            var response = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

        }

        [Test, TestCaseSource(typeof(PaymentsTestSource), nameof(PaymentsTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(PaymentModel payment, JudoModelErrorCode expectedModelErrorCode)
        {

            IResult<ITransactionResult> paymentReceiptResult = null;

            switch (payment)
            {
                // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
                case CardPaymentModel model:
                    paymentReceiptResult = JudoPayApiIridium.Payments.Create(model).Result;
                    break;
                case TokenPaymentModel model:
                    paymentReceiptResult = JudoPayApiIridium.Payments.Create(model).Result;
                    break;
                case OneTimePaymentModel model:
                    paymentReceiptResult = JudoPayApiIridium.Payments.Create(model).Result;
                    break;
                case PKPaymentModel model:
                    paymentReceiptResult = JudoPayApiIridium.Payments.Create(model).Result;
                    break;
            }
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            Assert.NotNull(paymentReceiptResult);
            Assert.IsTrue(paymentReceiptResult.HasError);
            Assert.IsNull(paymentReceiptResult.Response);
            Assert.IsNotNull(paymentReceiptResult.Error);
            Assert.AreEqual((int) JudoApiError.General_Model_Error, paymentReceiptResult.Error.Code);

            var fieldErrors = paymentReceiptResult.Error.ModelErrors;
            Assert.IsNotNull(fieldErrors);
            Assert.IsTrue(fieldErrors.Count >= 1);
            Assert.IsTrue(fieldErrors.Any(x => x.Code == (int) expectedModelErrorCode));
        }

        internal class PaymentsTestSource
        {
            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new CardPaymentModel
                    {
                        Amount = 1.20m,
                        CardNumber = "497600000003436",
                        CV2 = "452",
                        ExpiryDate = "12/25",
                        JudoId = "Invalid",
                        YourConsumerReference = "User10",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.JudoId_Not_Valid).SetName("ValidatePaymentInvalidJudoId");
                    yield return new TestCaseData(new CardPaymentModel
                    {
                        Amount = 1.20m,
                        CardNumber = null,
                        CV2 = "452",
                        ExpiryDate = "12/25",
                        JudoId = "100200302",
                        YourConsumerReference = "User10",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Card_Number_Not_Supplied).SetName("ValidatePaymentMissingCardNumber");
                    yield return new TestCaseData(new CardPaymentModel
                    {
                        Amount = 1.20m,
                        CardNumber = "497600000003436",
                        CV2 = "452",
                        ExpiryDate = "Invalid",
                        JudoId = "100200302",
                        YourConsumerReference = "User10",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Expiry_Date_Not_Valid).SetName("ValidatePaymentInvalidExpiryDate");
                    yield return new TestCaseData(new TokenPaymentModel
                    {
                        Amount = 1.20m,
                        CardToken = "",
                        CV2 = "123",
                        JudoId = "100200302",
                        YourConsumerReference = "User10",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Card_Token_Not_Supplied).SetName("ValidatePaymentInvalidCardToken");
                    yield return new TestCaseData(new TokenPaymentModel
                    {
                        Amount = 1.20m,
                        CardToken = "dh83MLZp87Io172Peqw2sWauWpy4oqfm",
                        CV2 = "123",
                        JudoId = "100200302",
                        YourConsumerReference = null,
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.Consumer_Reference_Not_Supplied_1).SetName("ValidatePaymentMissingConsumerReference");
                    yield return new TestCaseData(new OneTimePaymentModel
                    {
                        Amount = 2.0m,
                        OneUseToken = "",
                        JudoId = "100200302",
                        YourConsumerReference = "User10",
                        YourPaymentReference = "UniqueRef"
                    }, JudoModelErrorCode.EncryptedBlobNotSupplied).SetName("ValidatePaymentInvalidOneTimeToken");
                }
            }
        }

        [Test]
        public void TestPaymentWithInvalidThreeDSecureMpi()
        {
            // Given a payment model with an invalid ThreeDSecureMpiModel (one valid field)
            var paymentModel = GetCardPaymentModel();
            paymentModel.ThreeDSecureMpi = new ThreeDSecureMpiModel
            {
                Cavv = "",
                DsTransId = "",
                Eci = "",
                ThreeDSecureVersion = "2.1.0"
            };

            // When a payment is made with this model
            var result = JudoPayApiIridium.Payments.Create(paymentModel).Result;

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

        [Test]
        public async Task TestPaymentReceiptNoCardAddress()
        {
            // Given a payment without a card address
            var paymentWithCardNoAddress = GetCardPaymentNoCardAddressModel();
            var response = await JudoPayApiIridium.Payments.Create(paymentWithCardNoAddress);
            
            // Then the payment is successful 
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            // When we try to retrieve the receipt 
            var receipt = await JudoPayApiIridium.Transactions.Get(response.Response.ReceiptId);

            // Then there is no error 
            Assert.IsNotNull(receipt);
            Assert.IsFalse(receipt.HasError);
            Assert.AreEqual("Success", receipt.Response.Result);

            // And this is the same payment receipt 
            Assert.AreEqual(response.Response.ReceiptId, receipt.Response.ReceiptId);
        }

        [Test]
        public async Task TestCybersourcePaymentReceiptContainsPaymentNetworkTransactionId()
        {
            // Given a payment on a JudoId routed to Cybersource
            var paymentWithCard = GetCardPaymentModel(cardNumber: "4111111111111111",
                judoId: Configuration.CybersourceJudoId);
            var response = await JudoPayApiBase.Payments.Create(paymentWithCard);

            // Then the payment is successful 
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var receipt = response.Response as PaymentReceiptModel;
            // And the receipt contains a value for PaymentNetworkTransactionId
            Assert.IsTrue(!string.IsNullOrEmpty(receipt?.PaymentNetworkTransactionId));
        }
    }
}