using System.Net;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class ThreeDAuthorizationTests : IntegrationTestsBase
    {
        // Prod Sandbox transaction
        public CardPaymentModel PrepareThreeDSecureTwoCardPayment()
        {
            var paymentWithCard = GetCardPaymentModel();

            paymentWithCard.CardHolderName = "CHALLENGE";
            paymentWithCard.MobileNumber = "07999999999";
            paymentWithCard.PhoneCountryCode = "44";
            paymentWithCard.EmailAddress = "contact@judopay.com";

            paymentWithCard.UserAgent = "Mozilla/5.0,(Windows NT 6.1; WOW64),AppleWebKit/537.36,(KHTML, like Gecko)";
            paymentWithCard.AcceptHeaders = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp";

            paymentWithCard.ThreeDSecure = new ThreeDSecureTwoModel
            {
                AuthenticationSource = ThreeDSecureTwoAuthenticationSource.Browser,
                MethodNotificationUrl = "https://www.test.com",
                ChallengeNotificationUrl = "https://www.test.com",
                ChallengeRequestIndicator = ThreeDSecureTwoChallengeRequestIndicator.ChallengeAsMandate
            };

            return paymentWithCard;
        }

        [Test]
        public void PaymentWithThreeDSecureTwoRequiresDeviceDetailsCheck()
        {
            var paymentModel = PrepareThreeDSecureTwoCardPayment();

            var paymentResponse = JudoPayApiThreeDSecure2.Payments.Create(paymentModel).Result;
            Assert.IsNotNull(paymentResponse);

            if (paymentResponse.HasError)
            {
                if (paymentResponse.Error.Code == (int)HttpStatusCode.Forbidden)
                {
                    // Failed to authenticate - check your ApiToken and Secret
                }
                else if (paymentResponse.Error.ModelErrors != null)
                {
                    // Validation failed on the request, chech each list entry for details
                }
                else
                {
                    // Refer to https://docs.judopay.com/Content/Developer%20Tools/Codes.htm#Errors
                    var errorCode = paymentResponse.Error.Code;
                }
            }
            else if (paymentResponse.Response is PaymentReceiptModel receipt)
            {
                // Transaction was processed at gateway, check receipt for details
                var receiptId = receipt.ReceiptId;
                var status = receipt.Result;
                if (receipt.Result == "Success")
                {
                    var cardToken = receipt.CardDetails.CardToken;
                }
            }
            else if (paymentResponse.Response is PaymentRequiresThreeDSecureTwoModel threeDSecureTwoResponseModel)
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
        }

        [Test]
        public void PaymentWithThreedSecureTwoResumeRequest()
        {
            var paymentRequest = PrepareThreeDSecureTwoCardPayment();
            var paymentResponse = JudoPayApiThreeDSecure2.Payments.Create(paymentRequest).Result;
            Assert.IsNotNull(paymentResponse);
            Assert.IsFalse(paymentResponse.HasError);

            var paymentReceipt = paymentResponse.Response as PaymentRequiresThreeDSecureTwoModel;
            Assert.IsNotNull(paymentReceipt);
            Assert.AreEqual("Additional device data is needed for 3D Secure 2", paymentReceipt.Result);

            // Prepare the resume request once device details gathering happened 
            var resumeRequest = new ResumeThreeDSecureTwoModel
            {
                CV2 = "452",
                MethodCompletion = MethodCompletion.Yes
            };
            var resumeResponse = JudoPayApiThreeDSecure2.ThreeDs.Resume3DSecureTwo(
                paymentReceipt.ReceiptId, resumeRequest).Result;

            Assert.IsNotNull(resumeResponse);
            Assert.IsFalse(resumeResponse.HasError);

            var resumeReceipt = resumeResponse.Response as PaymentRequiresThreeDSecureTwoModel;

            Assert.IsNotNull(resumeReceipt);
            Assert.AreEqual("Challenge completion is needed for 3D Secure 2", resumeReceipt.Result);
            Assert.AreEqual("Issuer ACS has responded with a Challenge URL", resumeReceipt.Message);
            Assert.IsNotNull(resumeReceipt.ChallengeUrl);
            Assert.IsNotNull(resumeReceipt.Md);
            Assert.IsNotNull(resumeReceipt.Version);
            Assert.IsNotNull(resumeReceipt.CReq);

            Assert.IsNull(resumeReceipt.MethodUrl);
        }

        [Test]
        public void PaymentWithThreeDSecureTwoDirectChallengeRequest()
        {
            var paymentRequest = PrepareThreeDSecureTwoCardPayment();

            // Given a scenario that triggers a direct challenge without device details
            paymentRequest.CardHolderName = "FL-SUCCESS-NO-METHOD";

            // When the payment is created
            var paymentResponse = JudoPayApiThreeDSecure2.Payments.Create(paymentRequest).Result;
            Assert.IsNotNull(paymentResponse);
            Assert.IsFalse(paymentResponse.HasError);

            // Then the challenge fields are present in the response
            var paymentReceipt = paymentResponse.Response as PaymentRequiresThreeDSecureTwoModel;

            Assert.IsNotNull(paymentReceipt);
            Assert.AreEqual("Challenge completion is needed for 3D Secure 2", paymentReceipt.Result);
            Assert.AreEqual("Issuer ACS has responded with a Challenge URL", paymentReceipt.Message);
            Assert.IsNotNull(paymentReceipt.ChallengeUrl);
            Assert.IsNotNull(paymentReceipt.Version);
            Assert.IsNotNull(paymentReceipt.CReq);

            Assert.IsNull(paymentReceipt.MethodUrl);
        }

        [Test]
        public void FrictionlessPaymentWithThreeDSecureTwo()
        {
            var paymentRequest = PrepareThreeDSecureTwoCardPayment();

            // Given a scenario that triggers a direct challenge without device details 
            paymentRequest.CardHolderName = "FL-SUCCESSFUL-NO-METHOD";
            paymentRequest.ThreeDSecure.ChallengeRequestIndicator =
                ThreeDSecureTwoChallengeRequestIndicator.NoChallenge;

            // When the payment is created 
            var paymentResponse = JudoPayApiThreeDSecure2.Payments.Create(paymentRequest).Result;
            Assert.IsNotNull(paymentResponse);
            Assert.IsFalse(paymentResponse.HasError);

            // Then no device details or challenge is requested and a payment receipt is returned
            var paymentReceipt = paymentResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(paymentReceipt);
            Assert.AreEqual("Success", paymentReceipt.Result);
            Assert.IsNotNull(paymentReceipt.AuthCode);
        }

        [Explicit("Requires manual interaction to complete challenge")]
        public void PaymentWithThreeDSecureTwoCompleteRequest()
        {
            var paymentRequest = PrepareThreeDSecureTwoCardPayment();
            var paymentResponse = JudoPayApiThreeDSecure2.Payments.Create(paymentRequest).Result;

            Assert.IsNotNull(paymentResponse);
            Assert.IsFalse(paymentResponse.HasError);

            var paymentReceipt = paymentResponse.Response as PaymentRequiresThreeDSecureTwoModel;

            Assert.IsNotNull(paymentReceipt);
            Assert.AreEqual("Additional device data is needed for 3D Secure 2", paymentReceipt.Result);

            // Prepare the resume request once device details gathering happened 
            var resumeRequest = new ResumeThreeDSecureTwoModel
            {
                CV2 = "452",
                MethodCompletion = MethodCompletion.Yes
            };
            var resumeResponse = JudoPayApiThreeDSecure2.ThreeDs.Resume3DSecureTwo(
                paymentReceipt.ReceiptId, resumeRequest).Result;

            Assert.IsNotNull(resumeResponse);
            Assert.IsFalse(resumeResponse.HasError);

            var resumeReceipt = resumeResponse.Response as PaymentRequiresThreeDSecureTwoModel;

            Assert.IsNotNull(resumeReceipt);
            Assert.AreEqual("Challenge completion is needed for 3D Secure 2", resumeReceipt.Result);

            // Perform the challenge on the web browser using the information from the Resume

            // Then prepare the Complete request 
            var completeRequest = new CompleteThreeDSecureTwoModel { CV2 = "452" };
            var completeResponse = JudoPayApiThreeDSecure2.ThreeDs.Complete3DSecureTwo(
                paymentReceipt.ReceiptId, completeRequest).Result;

            Assert.IsNotNull(completeResponse);
            Assert.IsFalse(completeResponse.HasError);

            var completeReceipt = resumeResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(completeReceipt);
            Assert.IsNotNull(completeReceipt.JudoId);
            Assert.AreEqual("Success", completeReceipt.Result);
        }

        [Explicit("Requires manual interaction to complete challenge")]
        public void PaymentWithThreeDSecureTwoCompleteRequestWithAccountDetails()
        {
            var paymentRequest = PrepareThreeDSecureTwoCardPayment();
            var paymentResponse = JudoPayApiThreeDSecure2.Payments.Create(
                PrepareThreeDSecureTwoCardPayment()).Result;

            Assert.IsNotNull(paymentResponse);
            Assert.IsFalse(paymentResponse.HasError);

            var paymentReceipt = paymentResponse.Response as PaymentRequiresThreeDSecureTwoModel;

            Assert.IsNotNull(paymentReceipt);
            Assert.AreEqual("Additional device data is needed for 3D Secure 2", paymentReceipt.Result);

            // Prepare account details so they can be sent for follow-up requests 
            var accountDetails = new PrimaryAccountDetailsModel()
            {
                AccountNumber = "123456",
                DateOfBirth = "1980-01-01",
                Name = "John Smith",
                PostCode = "EC2A 4DP"
            };

            // Prepare the resume request containing account details
            var resumeRequest = new ResumeThreeDSecureTwoModel
            {
                CV2 = "452",
                MethodCompletion = MethodCompletion.Yes,
                PrimaryAccountDetails = accountDetails
            };

            var resumeResponse = JudoPayApiThreeDSecure2.ThreeDs.Resume3DSecureTwo(
                paymentReceipt.ReceiptId, resumeRequest).Result;

            Assert.IsNotNull(resumeResponse);
            Assert.IsFalse(resumeResponse.HasError);

            var resumeReceipt = resumeResponse.Response as PaymentRequiresThreeDSecureTwoModel;

            Assert.IsNotNull(resumeReceipt);
            Assert.AreEqual("Challenge completion is needed for 3D Secure 2", resumeReceipt.Result);

            // Perform the challenge on the web browser using the information from the Resume

            // Then prepare the Complete request containing account details
            var completeRequest = new CompleteThreeDSecureTwoModel
            {
                CV2 = "452",
                PrimaryAccountDetails = accountDetails
            };

            var completeResponse = JudoPayApiThreeDSecure2.ThreeDs.Complete3DSecureTwo(
                paymentReceipt.ReceiptId, completeRequest).Result;

            Assert.IsNotNull(completeResponse);
            Assert.IsFalse(completeResponse.HasError);

            var completeReceipt = resumeResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(completeReceipt);
            Assert.IsNotNull(completeReceipt.JudoId);
            Assert.AreEqual("Success", completeReceipt.Result);
        }

        [Test]
        public void CheckCardWithThreeDSecureTwoRequiresDeviceDetailsCheck()
        {
            // Given a CheckCard request model
            var checkCardPayment = GetCheckCardModel();

            checkCardPayment.CardHolderName = "CHALLENGE";
            checkCardPayment.MobileNumber = "07999999999";
            checkCardPayment.PhoneCountryCode = "44";
            checkCardPayment.EmailAddress = "contact@judopay.com";

            checkCardPayment.ThreeDSecure = new ThreeDSecureTwoModel
            {
                AuthenticationSource = ThreeDSecureTwoAuthenticationSource.Browser,
                MethodNotificationUrl = "https://www.test.com",
                ChallengeNotificationUrl = "https://www.test.com",
                ChallengeRequestIndicator = ThreeDSecureTwoChallengeRequestIndicator.NoPreference
            };

            // When a request to the CheckCards endpoint is made with a CardHolderName
            // that requires a device details check
            var checkCardResponse = JudoPayApiThreeDSecure2.CheckCards.Create(
                checkCardPayment).Result;

            Assert.IsNotNull(checkCardResponse);
            Assert.IsFalse(checkCardResponse.HasError);

            // Then a response indicating a 3DS2 device details check is required is received
            var deviceDetailsResponse = checkCardResponse.Response as PaymentRequiresThreeDSecureTwoModel;
            Assert.IsNotNull(deviceDetailsResponse);
            Assert.IsNotNull(deviceDetailsResponse.ReceiptId);
            Assert.IsNotNull(deviceDetailsResponse.MethodUrl);
            Assert.IsNotNull(deviceDetailsResponse.Md);
            Assert.IsNotNull(deviceDetailsResponse.Version);
            Assert.AreEqual("Additional device data is needed for 3D Secure 2", deviceDetailsResponse.Result);
            Assert.AreEqual("Issuer ACS has requested additional device data gathering", deviceDetailsResponse.Message);
        }
    }
}
