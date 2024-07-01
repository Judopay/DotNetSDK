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
    public class PreAuthAndCollectionTests : IntegrationTestsBase
    {
        [Test]
        public void ASimplePreAuth()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            MakeAPreAuthWithCard(paymentWithCard);
        }

        [Test]
        public void ADeclinedCardPreAuth()
        {
            var paymentWithCard = GetCardPaymentModel("432438862", "4221690000004963", "125");

            var response = JudoPayApiBase.PreAuths.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }

        [Test]
        public void ASimplePreAuthAndCollection()
        {
            // Given a successful preauth
            var paymentWithCard = GetCardPaymentModel();
            var preAuthReceiptId = MakeAPreAuthWithCard(paymentWithCard);

            var collection = new CollectionModel
            {
                ReceiptId = preAuthReceiptId,
                Amount = paymentWithCard.Amount
            };
            // When a full collection is requested
            var response = JudoPayApiBase.Collections.Create(collection).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            // Then collection is successful
            var receipt = response.Response as PaymentReceiptModel;
            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);

            // And details match initial preauth
            Assert.AreEqual(paymentWithCard.Amount, receipt.Amount);
            Assert.AreEqual(paymentWithCard.Currency, receipt.Currency);
            Assert.AreEqual(preAuthReceiptId, receipt.OriginalReceiptId);
        }

        [Test]
        public void PreAuthAndCollectionWithoutAmount()
        {
            var paymentWithCard = GetCardPaymentModel();
            var preAuthReceiptId = MakeAPreAuthWithCard(paymentWithCard);

            var collection = new CollectionModel
            {
                ReceiptId = preAuthReceiptId
            };

            var response = JudoPayApiBase.Collections.Create(collection).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("Collection", receipt.Type);
            Assert.AreEqual(paymentWithCard.Amount, receipt.Amount);
        }

        [Test]
        public void AFailedSimplePreAuthAndValidateCollection()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");
            var preAuthReceiptId = MakeAPreAuthWithCard(paymentWithCard);

            var collection = new CollectionModel
            {
                Amount = 20,
                ReceiptId = preAuthReceiptId
            };
            var collection2 = new CollectionModel
            {
                Amount = 20,
                ReceiptId = preAuthReceiptId
            };
            var collectionResponse = JudoPayApiBase.Collections.Create(collection).Result;

            // The collection will go through since it is less than the preauth amount
            Assert.That(collectionResponse.HasError, Is.False);
            Assert.NotNull(collectionResponse.Response.ReceiptId);

            var validateResponse = JudoPayApiBase.Collections.Create(collection2).Result;

            Assert.That(validateResponse, Is.Not.Null);
            Assert.That(validateResponse.HasError, Is.True);
            Assert.That(validateResponse.Error.Message, Is.EqualTo("Sorry, but the amount you're trying to collect is greater than the pre-auth"));
            Assert.That(validateResponse.Error.Code, Is.EqualTo(46));
        }

        [Test]
        public void ASimplePreAuthAndVoid()
        {
            var paymentWithCard = GetCardPaymentModel();
            var preAuthReceiptId = MakeAPreAuthWithCard(paymentWithCard);

            var voidPreAuth = new VoidModel
            {
                Amount = paymentWithCard.Amount,
                ReceiptId = preAuthReceiptId
            };

            var response = JudoPayApiBase.Voids.Create(voidPreAuth).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("VOID", receipt.Type);
            Assert.AreEqual(paymentWithCard.Amount, receipt.Amount);
        }


        [Test]
        public void PreAuthAndVoidWithoutAmount()
        {
            var paymentWithCard = GetCardPaymentModel();
            var preAuthReceiptId = MakeAPreAuthWithCard(paymentWithCard);

            var voidPreAuth = new VoidModel
            {
                ReceiptId = preAuthReceiptId
            };

            var response = JudoPayApiBase.Voids.Create(voidPreAuth).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("VOID", receipt.Type);
            Assert.AreEqual(paymentWithCard.Amount, receipt.Amount);
        }

        // Reuse Test source from PaymentTest as some model is used for payments/preauths
        [Test, TestCaseSource(typeof(PaymentTest.PaymentsTestSource), nameof(PaymentTest.PaymentsTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(PaymentModel preauth, JudoModelErrorCode expectedModelErrorCode)
        {
            IResult<ITransactionResult> preAuthReceiptResult = null;

            switch (preauth)
            {
                // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
                case CardPaymentModel model:
                    preAuthReceiptResult = JudoPayApiBase.PreAuths.Create(model).Result;
                    break;
                case TokenPaymentModel model:
                    preAuthReceiptResult = JudoPayApiBase.PreAuths.Create(model).Result;
                    break;
                case ApplePayPaymentModel model:
                    preAuthReceiptResult = JudoPayApiBase.PreAuths.Create(model).Result;
                    break;
                case GooglePayPaymentModel model:
                    preAuthReceiptResult = JudoPayApiBase.PreAuths.Create(model).Result;
                    break;
            }
            // ReSharper restore CanBeReplacedWithTryCastAndCheckForNull

            Assert.NotNull(preAuthReceiptResult);
            Assert.IsTrue(preAuthReceiptResult.HasError);
            Assert.IsNull(preAuthReceiptResult.Response);
            Assert.IsNotNull(preAuthReceiptResult.Error);
            Assert.AreEqual((int)JudoApiError.General_Model_Error, preAuthReceiptResult.Error.Code);

            var fieldErrors = preAuthReceiptResult.Error.ModelErrors;
            Assert.IsNotNull(fieldErrors);
            Assert.IsTrue(fieldErrors.Count >= 1);
            Assert.IsTrue(fieldErrors.Any(x => x.Code == (int)expectedModelErrorCode));
        }

        private string MakeAPreAuthWithCard(CardPaymentModel paymentWithCard)
        {
            var response = JudoPayApiBase.PreAuths.Create(paymentWithCard).Result;
            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;
            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);
            Assert.AreEqual("PreAuth", receipt.Type);

            return receipt.ReceiptId;
        }

        [Test]
        public async Task TestDocs()
        {
            var client = JudoPayApiThreeDSecure2;

            var consumerRef = Guid.NewGuid().ToString();
            var paymentRef = Guid.NewGuid().ToString();
            //Create an instance of the CardPayment Model
            var preauthRequest = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = consumerRef,
                YourPaymentReference = paymentRef,
                CardNumber = "4976000000003436",
                ExpiryDate = "12/25",
                CV2 = "452",
                Amount = 1.01m,
                Currency = "GBP",
                // PrimaryAccountDetails only required for MCC6012 merchants
                PrimaryAccountDetails = new PrimaryAccountDetailsModel
                {
                    Name = "Smith",
                    AccountNumber = "1234567",
                    DateOfBirth = "2000-12-31",
                    PostCode = "EC2A 4DP"
                },
                // Following are for 3DS2 transactions
                PhoneCountryCode = "44",
                MobileNumber = "7999123456",
                EmailAddress = "test.user@judopay.com",
                CardHolderName = "John Smith",
                ThreeDSecure = new ThreeDSecureTwoModel
                {
                    AuthenticationSource = ThreeDSecureTwoAuthenticationSource.Browser,
                    ChallengeRequestIndicator = ThreeDSecureTwoChallengeRequestIndicator.ChallengeAsMandate,
                    MethodNotificationUrl = "https://yourMethodNotificationUrl",
                    ChallengeNotificationUrl = "https://yourChallengeNotificationUrl"
                }
            };

            //Send the request to Judopay
            var response = await client.PreAuths.Create(preauthRequest);

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
            else if (response.Response is PaymentRequiresThreeDSecureTwoModel threeDSecureTwoResponseModel)
            {
                if (threeDSecureTwoResponseModel.MethodUrl != null)
                {
                    // Device details are required - POST md as threeDSMethodData to methodUrl
                    var methodUrl = threeDSecureTwoResponseModel.MethodUrl;
                    var md = threeDSecureTwoResponseModel.Md;
                }
                else if (threeDSecureTwoResponseModel.ChallengeUrl != null)
                {
                    // Challenge is required - POST creq to challengeUrl
                    var challengeUrl = threeDSecureTwoResponseModel.ChallengeUrl;
                    var creq = threeDSecureTwoResponseModel.CReq;
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

        [Test]
        public void TestDelayedAuthorisation()
        {
            var preAuthWithCard = PrepareThreeDSecureTwoCardPayment();
            preAuthWithCard.CardHolderName = "FL-SUCCESS-NO-METHOD";
            preAuthWithCard.ThreeDSecure = new ThreeDSecureTwoModel
            {
                AuthenticationSource = ThreeDSecureTwoAuthenticationSource.Browser,
                ChallengeRequestIndicator = ThreeDSecureTwoChallengeRequestIndicator.NoPreference
            };
            preAuthWithCard.DelayedAuthorisation = true;

            var preAuthResponse = JudoPayApiThreeDSecure2.PreAuths.Create(preAuthWithCard).Result;
            Assert.IsNotNull(preAuthResponse);
            Assert.IsFalse(preAuthResponse.HasError);

            var preAuthReceipt = preAuthResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(preAuthReceipt);
            Assert.AreEqual("Success", preAuthReceipt.Result);
            Assert.AreEqual("Successful authentication - Pending delayed authorisation",
                preAuthReceipt.Message);

            var collection = new CollectionModel
            {
                ReceiptId = preAuthReceipt.ReceiptId,
                Amount = preAuthWithCard.Amount
            };

            var collectResponse = JudoPayApiBase.Collections.Create(collection).Result;

            Assert.IsNotNull(collectResponse);
            Assert.IsFalse(collectResponse.HasError);

            var collectReceipt = collectResponse.Response as PaymentReceiptModel;

            Assert.IsNotNull(collectReceipt);

            Assert.AreEqual("Success", collectReceipt.Result);
            Assert.AreEqual("Collection", collectReceipt.Type);
            Assert.AreEqual(preAuthReceipt.Amount, collectReceipt.Amount);
        }

        [Test]
        public void PreAuthIncrementalAuthAndCollectionWithoutAmount()
        {
            var preAuthWithCard = GetCardPaymentModel(judoId: Configuration.CybersourceJudoId);
            preAuthWithCard.Amount = 1.01m;
            preAuthWithCard.AllowIncrement = true;

            var preAuthResponse = JudoPayApiBase.PreAuths.Create(preAuthWithCard).Result;
            Assert.IsNotNull(preAuthResponse);
            Assert.IsFalse(preAuthResponse.HasError);

            var preAuthReceipt = preAuthResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(preAuthReceipt);
            Assert.AreEqual("Success", preAuthReceipt.Result);
            Assert.AreEqual("PreAuth", preAuthReceipt.Type);
            Assert.IsTrue(preAuthReceipt.AllowIncrement);
            Assert.IsNull(preAuthReceipt.IsIncrementalAuth);

            var preAuthReceiptId = preAuthReceipt.ReceiptId;

            var incrementAuthRequest = new IncrementalAuthModel
            {
                ReceiptId = preAuthReceiptId,
                Amount = 0.10m,
                YourPaymentReference = "Increment of " + preAuthReceiptId
            };

            var incrementAuthResponse = JudoPayApiBase.PreAuths.IncrementAuth(incrementAuthRequest).Result;
            Assert.IsNotNull(incrementAuthResponse);
            Assert.IsFalse(incrementAuthResponse.HasError);

            var incrementAuthReceipt = incrementAuthResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(incrementAuthReceipt);
            Assert.AreEqual("Success", incrementAuthReceipt.Result);
            Assert.AreEqual("PreAuth", incrementAuthReceipt.Type);
            Assert.IsTrue(incrementAuthReceipt.IsIncrementalAuth);
            Assert.IsNull(incrementAuthReceipt.AllowIncrement);
            Assert.AreEqual(0.10m, incrementAuthReceipt.Amount);
            Assert.AreEqual(1.11m, incrementAuthReceipt.NetAmount);

            var collectionRequest = new CollectionModel
            {
                ReceiptId = preAuthReceiptId
            };

            var collectionResponse = JudoPayApiBase.Collections.Create(collectionRequest).Result;
            Assert.IsNotNull(collectionResponse);
            Assert.IsFalse(collectionResponse.HasError);

            var collectionReceipt = collectionResponse.Response as PaymentReceiptModel;

            Assert.IsNotNull(collectionReceipt);

            Assert.AreEqual("Success", collectionReceipt.Result);
            Assert.AreEqual("Collection", collectionReceipt.Type);
            Assert.AreEqual(incrementAuthReceipt.NetAmount, collectionReceipt.Amount);
        }
    }
}