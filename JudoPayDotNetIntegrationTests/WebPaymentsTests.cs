using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
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
        public void TransactionsGetByReceiptId()
        {
            var request = GetWebPaymentRequestModel();
            var result = JudoPayApiIridium.WebPayments.Payments.Create(request).Result;
            var reference = result.Response.Reference;

            // Forms - Post a form with credentials to post url from the webpayment response passing form parameter Reference
            var httpClient = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("Reference", reference)
            });

            // The test works only with webpayments v1, so we need to alter the URL version if the account is set to use v2 
            result.Response.PostUrl = result.Response.PostUrl.Replace("/v2", "/v1");

            var formRequest = CreateJudoApiRequest(result.Response.PostUrl, HttpMethod.Post, "6.7.0.0", Configuration.ElevatedPrivilegesToken, Configuration.ElevatedPrivilegesSecret);

            formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            formRequest.Content = formContent;

            var paymentPage = httpClient.SendAsync(formRequest).Result;

            var resultBody = paymentPage.Content.ReadAsStringAsync().Result;
            var doc = new HtmlDocument();
            doc.LoadHtml(resultBody);

            // Forms - Post a form with credentials and cookie and form following variables:
            var formField = doc.DocumentNode.SelectSingleNode("//input[@name='__RequestVerificationToken']");

            var requestVerificationToken = formField.GetAttributeValue("value", "");
            formContent = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("__RequestVerificationToken", requestVerificationToken),
                new KeyValuePair<string, string>("CardNumber", "4976000000003436"),
                new KeyValuePair<string, string>("Cv2", "452"), 
                new KeyValuePair<string, string>("CardAddress.CountryCode", "826"), 
                new KeyValuePair<string, string>("CardAddress.PostCode", "TR14 8PA"), 
                new KeyValuePair<string, string>("ExpiryDate", "12/25"), 
                new KeyValuePair<string, string>("Reference", reference),
                new KeyValuePair<string, string>("YourConsumerReference", "4235325"), 
            });

            formRequest = CreateJudoApiRequest(Configuration.WebpaymentsUrl, HttpMethod.Post, "6.7.0.0", Configuration.ElevatedPrivilegesToken, Configuration.ElevatedPrivilegesSecret);

            formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            formRequest.Content = formContent;

            var resultPage = httpClient.SendAsync(formRequest).Result;

            resultBody = resultPage.Content.ReadAsStringAsync().Result;

            // Retrieve from the response the receipt id
            doc = new HtmlDocument();
            doc.LoadHtml(resultBody);

            formField = doc.DocumentNode.SelectSingleNode("//input[@name='ReceiptId']");

            var receiptId = formField.GetAttributeValue("value", "");

            var webRequest = JudoPayApiIridium.WebPayments.Transactions.GetByReceipt(receiptId).Result;

            Assert.NotNull(webRequest);
            Assert.IsFalse(webRequest.HasError);
            Assert.NotNull(webRequest.Response);
            Assert.NotNull(webRequest.Response.Reference);
            Assert.AreEqual(request.JudoId, webRequest.Response.JudoId);
        }

        private HttpRequestMessage CreateJudoApiRequest(string url, HttpMethod method, string apiVersion, string apiToken, string apiSecret)
        {
            var request = new HttpRequestMessage(method, url);

            var full = string.Format("{0}:{1}", apiToken, apiSecret);

            var authDetails = Encoding.GetEncoding("iso-8859-1").GetBytes(full);
            var parameter = Convert.ToBase64String(authDetails);

            request.Headers.Add("Accept", "application/json");

            request.Headers.Add("Api-Version", apiVersion);

            request.Headers.Add("User-Agent", "Mozilla");

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", parameter);

            return request;
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

            var response = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            // Can't check WebPaymentReference on receipt yet (until JR-4659 implemented)
        }

        [Test, TestCaseSource(typeof(WebPaymentsTestSource), nameof(WebPaymentsTestSource.ValidateFailureTestCases))]
        public void ValidateWithoutSuccess(WebPaymentRequestModel webPayment, JudoModelErrorCode expectedModelErrorCode)
        {
            var webPaymentResult = JudoPayApiIridium.WebPayments.Payments.Create(webPayment).Result;

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
