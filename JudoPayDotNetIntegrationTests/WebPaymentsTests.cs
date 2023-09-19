using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class WebPaymentsTests : IntegrationTestsBase
    {
        [Test]
        public void PaymentCreate()
        {
            var request = GetWebPaymentRequestModel();

            var result = JudoPayApiElevated.WebPayments.Payments.Create(request).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.NotNull(result.Response);
            Assert.NotNull(result.Response.Reference);
            Assert.NotNull(result.Response.PostUrl);
        }

        [Test]
        public void ThreeDSecurePaymentCreate()
        {
            var request = Get3ds2WebPaymentRequestModel();

            var result = JudoPayApiElevated.WebPayments.Payments.Create(request).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.NotNull(result.Response);
            Assert.NotNull(result.Response.Reference);
            Assert.NotNull(result.Response.PostUrl);
        }

        [Test]
        public void PreAuthCreate()
        {
            var request = GetWebPaymentRequestModel();

            var result = JudoPayApiElevated.WebPayments.PreAuths.Create(request).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.NotNull(result.Response);
            Assert.NotNull(result.Response.Reference);
            Assert.NotNull(result.Response.PostUrl);
        }

        [Test]
        public void CheckCardCreate()
        {
            var request = GetWebPaymentRequestModel();

            var result = JudoPayApiElevated.WebPayments.CheckCards.Create(request).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.NotNull(result.Response);
            Assert.NotNull(result.Response.Reference);
            Assert.NotNull(result.Response.PostUrl);
        }

        [Test]
        public void GetWebPaymentByReference()
        {
            var request = Get3ds2WebPaymentRequestModel();
            var yourMetaData = new Dictionary<string, object>
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };
            request.YourPaymentMetaData = yourMetaData;
            request.HideBillingInfo = true;
            request.HideReviewInfo = true;
            request.PrimaryAccountDetails = new PrimaryAccountDetailsModel
            {
                Name = "Smith",
                AccountNumber = "12345678",
                PostCode = "AB1 2CD",
                DateOfBirth = "1980-01-01"
            };

            var createResult = JudoPayApiElevated.WebPayments.Payments.Create(request).Result;

            Assert.NotNull(createResult);
            Assert.IsFalse(createResult.HasError);

            var createResponse = createResult.Response;
            Assert.NotNull(createResponse);
            Assert.NotNull(createResponse.Reference);
            Assert.NotNull(createResponse.PostUrl);

            var getResult = JudoPayApiElevated.WebPayments.Transactions.Get(createResponse.Reference).Result;

            Assert.NotNull(getResult);
            Assert.IsFalse(getResult.HasError);
            Assert.NotNull(getResult.Response);

            var getResponse = getResult.Response;

            Assert.AreEqual(createResponse.Reference, getResponse.Reference);
            Assert.AreEqual(request.JudoId, getResponse.JudoId);
            Assert.AreEqual(request.Amount, getResponse.Amount);
            Assert.AreEqual(request.Currency, getResponse.Currency);
            Assert.AreEqual(request.CardAddress.CardHolderName, getResponse.CardAddress.CardHolderName);
            Assert.AreEqual(request.CardAddress.Address1, getResponse.CardAddress.Address1);
            Assert.AreEqual(request.CardAddress.Address2, getResponse.CardAddress.Address2);
            Assert.AreEqual(request.CardAddress.Address2, getResponse.CardAddress.Address2);
            Assert.AreEqual(request.CardAddress.Town, getResponse.CardAddress.Town);
            Assert.AreEqual(request.CardAddress.PostCode, getResponse.CardAddress.PostCode);
            Assert.AreEqual(request.CardAddress.CountryCode, getResponse.CardAddress.CountryCode);
            Assert.AreEqual(request.CardAddress.State, getResponse.CardAddress.State);
            Assert.NotNull(getResponse.ExpiryDate); // ExpiryDate is rounded to nearest millisecond from request
            Assert.AreEqual(request.CancelUrl, getResponse.PaymentCancelUrl);
            Assert.AreEqual(request.SuccessUrl, getResponse.PaymentSuccessUrl);
            Assert.AreEqual(request.YourConsumerReference, getResponse.YourConsumerReference);
            Assert.AreEqual(request.YourPaymentReference, getResponse.YourPaymentReference);
            Assert.AreEqual(request.YourPaymentMetaData, getResponse.YourPaymentMetaData);
            Assert.AreEqual(WebPaymentOperation.Payment, getResponse.WebPaymentOperation);
            Assert.AreEqual(request.IsPayByLink, getResponse.IsPayByLink);
            Assert.AreEqual(request.MobileNumber, getResponse.MobileNumber);
            Assert.AreEqual(request.PhoneCountryCode, getResponse.PhoneCountryCode);
            Assert.AreEqual(request.EmailAddress, getResponse.EmailAddress);
            Assert.IsNull(getResponse.HideBillingInfo); // Not returned in response, but used by WebPayments
            Assert.IsNull(getResponse.HideReviewInfo); // Not returned in response, but used by WebPayments
            Assert.IsNull(getResponse.ThreeDSecure); // Not currently returned on response
            Assert.IsNull(getResponse.PrimaryAccountDetails);
            Assert.NotNull(getResponse.CompanyName);
            Assert.NotNull(getResponse.AllowedCardTypes);
            Assert.NotNull(getResponse.Response);
            Assert.AreEqual(createResponse.Reference, getResponse.Response.Reference);
            Assert.AreEqual(WebPaymentStatus.Open, getResponse.Status);
            Assert.AreEqual(TransactionType.PAYMENT, getResponse.TransactionType);
            Assert.IsNull(getResponse.Receipt);
            Assert.AreEqual(0, getResponse.NoOfAuthAttempts);
        }

        [Test]
        public async Task CreatePaymentSessionThenPayWithReference()
        {
            var request = GetWebPaymentRequestModel();

            var result = JudoPayApiElevated.WebPayments.Payments.Create(request).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.NotNull(result.Response);
            Assert.NotNull(result.Response.Reference);
            Assert.NotNull(result.Response.PostUrl);

            // Send payment request with WebPaymentReference set
            var paymentWithCard = GetCardPaymentModel();
            paymentWithCard.WebPaymentReference = result.Response.Reference;

            // Set other fields to be identical for the authentication to be successful 
            paymentWithCard.Amount = request.Amount;
            paymentWithCard.YourConsumerReference = request.YourConsumerReference;
            paymentWithCard.YourPaymentReference = request.YourPaymentReference;

            var response = await JudoPayApiBase.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            // Can't check WebPaymentReference on receipt yet (until JR-4659 implemented)
        }

        [Test, TestCaseSource(typeof(WebPaymentsTestSource), nameof(WebPaymentsTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(WebPaymentRequestModel webPayment, JudoModelErrorCode expectedModelErrorCode)
        {
            var webPaymentResult = JudoPayApiBase.WebPayments.Payments.Create(webPayment).Result;

            Assert.NotNull(webPaymentResult);
            Assert.IsTrue(webPaymentResult.HasError);
            Assert.IsNull(webPaymentResult.Response);
            Assert.IsNotNull(webPaymentResult.Error);
            Assert.AreEqual((int)JudoApiError.General_Model_Error, webPaymentResult.Error.Code);

            var fieldErrors = webPaymentResult.Error.ModelErrors;
            Assert.IsNotNull(fieldErrors);
            Assert.IsTrue(fieldErrors.Count >= 1);
            Assert.IsTrue(fieldErrors.Any(x => x.Code == (int)expectedModelErrorCode));
        }

        [Test]
        public async Task SampleCreationPaymentSessionFor3DS2()
        {
            var client = JudoPaymentsFactory.Create(
                        JudoEnvironment.Sandbox, "ApiToken", "ApiSecret");

            var paymentSessionRequestModel = new WebPaymentRequestModel
            {
                JudoId = "100100100",
                YourConsumerReference = "3f7064d3-43e4-45a9-8329-2c4b9dd4d9e0",
                // A guid will be generated for YourPaymentReference
                YourPaymentMetaData = new Dictionary<string, object>
                {
                    { "key1", "value1" },
                    { "key2", "value2" }
                },
                Amount = 1.01m,
                Currency = "GBP",
                ExpiryDate = DateTimeOffset.Now.AddHours(24),
                ThreeDSecure = new ThreeDSecureTwoModel
                {
                    AuthenticationSource = ThreeDSecureTwoAuthenticationSource.Browser,
                    ChallengeRequestIndicator = ThreeDSecureTwoChallengeRequestIndicator.ChallengeAsMandate
                }
                // PrimaryAccountDetails should also be set for MCC-6012 merchants
            };
            
            var webPaymentResult = await client.WebPayments.Payments.Create(paymentSessionRequestModel);

            if (webPaymentResult.HasError)
            {
                // HandleError
            }
            else
            {
                var paymentSessionReference = webPaymentResult.Response.Reference;
                var yourPaymentReference = paymentSessionRequestModel.YourPaymentReference;
            }
        }

        [Test]
        public void PaymentSessionCancel()
        {
            var request = GetWebPaymentRequestModel();

            var result = JudoPayApiElevated.WebPayments.Payments.Create(request).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.NotNull(result.Response);
            var reference = result.Response.Reference;

            var cancelResult = JudoPayApiElevated.WebPayments.Payments.Cancel(reference).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(WebPaymentStatus.Cancelled, cancelResult.Response.Status);
            Assert.AreEqual(result.Response.Reference, cancelResult.Response.Reference);
        }

        [Test]
        public void PreAuthSessionCancel()
        {
            var request = GetWebPaymentRequestModel();

            var result = JudoPayApiElevated.WebPayments.PreAuths.Create(request).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.NotNull(result.Response);
            var reference = result.Response.Reference;

            var cancelResult = JudoPayApiElevated.WebPayments.PreAuths.Cancel(reference).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(WebPaymentStatus.Cancelled, cancelResult.Response.Status);
            Assert.AreEqual(result.Response.Reference, cancelResult.Response.Reference);
        }

        [Test]
        public void CheckCardSessionCancel()
        {
            var request = GetWebPaymentRequestModel();

            var result = JudoPayApiElevated.WebPayments.CheckCards.Create(request).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.NotNull(result.Response);
            var reference = result.Response.Reference;

            var cancelResult = JudoPayApiElevated.WebPayments.CheckCards.Cancel(reference).Result;

            Assert.NotNull(result);
            Assert.IsFalse(result.HasError);
            Assert.AreEqual(WebPaymentStatus.Cancelled, cancelResult.Response.Status);
            Assert.AreEqual(result.Response.Reference, cancelResult.Response.Reference);
        }

        [Test]
        public void GetShortReferenceFromWebPayment()
        {
            // Given a WebPayment session
            var yourConsumerReference = "432438862";
            var webPaymentRequest = new WebPaymentRequestModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                Amount = 25
            };

            var webPaymentResult = JudoPayApiBase.WebPayments.Payments.Create(webPaymentRequest).Result;
            Assert.NotNull(webPaymentResult);
            Assert.IsFalse(webPaymentResult.HasError);

            var webPaymentReference = webPaymentResult.Response.Reference;
            Assert.NotNull(webPaymentReference);

            var shortUrl = webPaymentResult.Response.ShortUrl;

            // When session is retrieved by reference
            var paymentSession = JudoPayApiBase.WebPayments.Transactions.Get(
                webPaymentReference).Result;

            Assert.IsNotNull(paymentSession);
            Assert.IsFalse(paymentSession.HasError);

            // Then shortReference on payment session matches end of shortUrl from response
            Assert.IsNotNull(paymentSession.Response.ShortReference);
            var shortRef = shortUrl?.Substring(shortUrl.LastIndexOf('/') + 1);
            Assert.AreEqual(shortRef,
                paymentSession.Response.ShortReference);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        [TestCase(null)]
        public void GetDelayedAuthorisationFromWebPayment(bool? delayedAuthorisation)
        {
            // Given a WebPayment session
            var yourConsumerReference = "432438862";
            var webPaymentRequest = new WebPaymentRequestModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                Amount = 25,
                DelayedAuthorisation = delayedAuthorisation
            };

            // For a PreAuth
            var webPaymentResult = JudoPayApiBase.WebPayments.PreAuths.Create(webPaymentRequest).Result;
            Assert.NotNull(webPaymentResult);
            Assert.IsFalse(webPaymentResult.HasError);
            var webPaymentReference = webPaymentResult.Response.Reference;
            Assert.NotNull(webPaymentReference);

            // When session is retrieved by reference
            var paymentSession = JudoPayApiBase.WebPayments.Transactions.Get(
                webPaymentReference).Result;

            Assert.IsNotNull(paymentSession);
            Assert.IsFalse(paymentSession.HasError);

            // Then delayedAuthorisation on respones matches that on session
            Assert.AreEqual(delayedAuthorisation, paymentSession.Response.DelayedAuthorisation);
        }

        [Test]
        public void GetReceiptFromCompletedWebPayment()
        {
            // Given a WebPayment session
            var yourConsumerReference = "432438862";
            var webPaymentRequest = new WebPaymentRequestModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                Amount = 25
            };

            var webPaymentResult = JudoPayApiBase.WebPayments.Payments.Create(webPaymentRequest).Result;
            Assert.NotNull(webPaymentResult);
            Assert.IsFalse(webPaymentResult.HasError);

            var webPaymentReference = webPaymentResult.Response.Reference;
            Assert.NotNull(webPaymentReference);

            // And an associated payment (passing webPaymentReference)
            var paymentWithCard = GetCardPaymentModel();
            paymentWithCard.WebPaymentReference = webPaymentReference;

            // Set other fields to be identical for the authentication to be successful 
            paymentWithCard.Amount = webPaymentRequest.Amount;
            paymentWithCard.YourConsumerReference = webPaymentRequest.YourConsumerReference;
            paymentWithCard.YourPaymentReference = webPaymentRequest.YourPaymentReference;

            var paymentResponse = JudoPayApiBase.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(paymentResponse);
            Assert.IsFalse(paymentResponse.HasError);
            Assert.AreEqual("Success", paymentResponse.Response.Result);

            // When session is retrieved by reference
            var paymentSession = JudoPayApiBase.WebPayments.Transactions.Get(
                webPaymentReference).Result;

            Assert.IsNotNull(paymentSession);
            Assert.IsFalse(paymentSession.HasError);

            // Then receipt on payment session matches that of transaction
            Assert.IsNotNull(paymentSession.Response.Receipt);
            Assert.AreEqual(paymentResponse.Response.ReceiptId,
                paymentSession.Response.Receipt.ReceiptId);
        }


        internal class WebPaymentsTestSource
        {
            public static IEnumerable ValidateFailureTestCases
            {
                get
                {
                    yield return new TestCaseData(new WebPaymentRequestModel
                    {
                        Amount = 1.20m,
                        JudoId = "Invalid",
                        YourConsumerReference = "User10"
                    }, JudoModelErrorCode.JudoId_Not_Valid_1).SetName("ValidateWebPaymentInvalidJudoId");
                    yield return new TestCaseData(new WebPaymentRequestModel
                    {
                        Amount = 1.20m,
                        JudoId = null,
                        YourConsumerReference = "User10"
                    }, JudoModelErrorCode.JudoId_Not_Supplied_1).SetName("ValidateWebPaymentMissingJudoId");
                    yield return new TestCaseData(new WebPaymentRequestModel
                    {
                        Amount = 1.20m,
                        JudoId = "100200302",
                        YourConsumerReference = null
                    }, JudoModelErrorCode.Consumer_Reference_Not_Supplied).SetName("ValidateWebPaymentMissingConsumerReference");
                    yield return new TestCaseData(new WebPaymentRequestModel
                    {
                        Amount = 1.20m,
                        JudoId = "100200302",
                        YourConsumerReference = ""
                    }, JudoModelErrorCode.Consumer_Reference_Not_Supplied).SetName("ValidateWebPaymentEmptyConsumerReference");
                    // Amount is not mandatory for WebPayments;
                    yield return new TestCaseData(new WebPaymentRequestModel
                    {
                        Amount = -1m,
                        JudoId = "100200302",
                        YourConsumerReference = "User10"
                    }, JudoModelErrorCode.Amount_Must_Be_Greater_Than_Or_Equal_To_0).SetName("ValidateWebPaymentNegativeAmount");
                }
            }
        }
    }
}
