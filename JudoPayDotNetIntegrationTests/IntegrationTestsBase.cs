using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using Newtonsoft.Json;

namespace JudoPayDotNetIntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        protected JudoPayApi JudoPayApiIridium;
        protected JudoPayApi JudoPayApiCyberSource;
        protected JudoPayApi JudoPayApiElevated;
        protected readonly Configuration Configuration = new Configuration();

        protected IntegrationTestsBase() 
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            JudoPayApiIridium = JudoPaymentsFactory.Create(
                Configuration.JudoEnvironment, 
                Configuration.Iridium_Token, 
                Configuration.Iridium_Secret
            );

            JudoPayApiCyberSource = JudoPaymentsFactory.Create(
                Configuration.JudoEnvironment, 
                Configuration.Cybersource_Token, 
                Configuration.Cybersource_Secret
            );

            JudoPayApiElevated = JudoPaymentsFactory.Create(
                Configuration.JudoEnvironment, 
                Configuration.ElevatedPrivilegesToken, 
                Configuration.ElevatedPrivilegesSecret
            );
        }

        protected CardPaymentModel GetCardPaymentModel(
            string judoId,
            string yourConsumerReference = null, 
            string cardNumber = "4976000000003436", 
            string cv2 = "452", 
            string postCode = "TR14 8PA", 
            bool? recurringPayment = null)
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            return new CardPaymentModel
            {
                JudoId = judoId,
                YourConsumerReference = yourConsumerReference,
                Amount = 25,
                CardNumber = cardNumber,
                CV2 = cv2,
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "32 Edward Street",
                    PostCode = postCode,
                    Town = "Camborne"
                },
                RecurringPayment = recurringPayment
            };
        }

        protected RegisterCardModel GetRegisterCardModel(
            string judoId,
            string yourConsumerReference = null,
            string cardNumber = "4976000000003436",
            string cv2 = "452",
            string postCode = "TR14 8PA",
            bool? recurringPayment = null
            )
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            return new RegisterCardModel
            {
                JudoId = judoId,
                YourConsumerReference = yourConsumerReference,
                CardNumber = cardNumber,
                CV2 = cv2,
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "32 Edward Street",
                    PostCode = postCode,
                    Town = "Camborne"
                }
            };
        }

        protected TokenPaymentModel GetTokenPaymentModel(
            string cardToken,
            string judoId,
            string yourConsumerReference = null, 
            decimal amount = 25,
            bool? recurringPayment = null)
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            return new TokenPaymentModel
            {
                JudoId = judoId,
                YourConsumerReference = yourConsumerReference,
                Amount = amount,
                CardToken = cardToken,
                CV2 = "452",
                ConsumerToken = "ABSE",
                RecurringPayment = recurringPayment
            };
        }

        protected CheckCardModel GetCheckCardModel(
            string judoId, 
            string cardNumber = "4976000000003436", 
            string cv2 = "452")
        {
           var yourConsumerReference = Guid.NewGuid().ToString();

            return new CheckCardModel
            {
                JudoId = judoId,
                YourConsumerReference = yourConsumerReference,
                CardNumber = cardNumber,
                CV2 = cv2,
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "32 Edward Street",
                    PostCode = "TR14 8PA",
                    Town = "Camborne"
                }
            };
        }

        protected async Task<OneTimePaymentModel> GetOneTimePaymentModel(string judoId, string authorisationToken)
        {
            var oneUseTokenModel = await GetOneUseToken(authorisationToken);

            return new OneTimePaymentModel
            {
                OneUseToken = oneUseTokenModel.OneUseToken,
                JudoId = judoId,
                YourConsumerReference = Guid.NewGuid().ToString(),
                Amount = 25,
                CardAddress = new CardAddressModel
                {
                    Line1 = "32 Edward Street",
                    PostCode = "TR14 8PA",
                    Town = "Camborne"
                }
            };
        }

        private async Task<OneUseTokenModel> GetOneUseToken(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Api-Version", VersioningHandler.DEFAULT_API_VERSION);
            client.DefaultRequestHeaders.Add("Authorization", $"Simple {token}");

            var cardDetailsModel = new Dictionary<string, string>
            {
                {"cardNumber", "4976000000003436"},
                {"expiryDate", "1220"},
                {"cV2", "452"},
            };
            var message = new HttpRequestMessage
            {
                Content = new StringContent(JsonConvert.SerializeObject(cardDetailsModel), Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
                RequestUri = new Uri(JudoPaymentsFactory.GetEnvironmentUrl(Configuration.JudoEnvironment) + "/encryptions/paymentdetails")
            };

            var response = await client.SendAsync(message);
            var oneUseTokenModel = JsonConvert.DeserializeObject<OneUseTokenModel>(await response.Content.ReadAsStringAsync());
            return oneUseTokenModel;
        }

        private class OneUseTokenModel
        {
            public string OneUseToken { get; set; }
        }

        protected WebPaymentRequestModel GetWebPaymentRequestModel(string judoId)
        {
            return new WebPaymentRequestModel
            {
                Amount = 10,
                CardAddress = new WebPaymentCardAddress
                {
                    CardHolderName = "Test User",
                    Line1 = "Test Street",
                    Line2 = "Test Street",
                    Line3 = "Test Street",
                    Town = "London",
                    PostCode = "W31 4HS",
                    Country = "England"
                },
                ClientIpAddress = "127.0.0.1",
                CompanyName = "Test",
                Currency = "GBP",
                ExpiryDate = DateTimeOffset.Now,
                JudoId = judoId,
                PartnerServiceFee = 10,
                PaymentCancelUrl = "http://test.com",
                PaymentSuccessUrl = "http://test.com",
                Reference = "42421",
                Status = WebPaymentStatus.Open,
                TransactionType = TransactionType.PAYMENT,
                YourConsumerReference = "4235325"
            };
        }

        protected VisaCheckoutPaymentModel GetVisaCheckoutPaymentModel(
            string judoId,
            string callId, 
            string encKey, 
            string encPaymentData, 
            string yourConsumerReference = "Consumer1")
        {
            return new VisaCheckoutPaymentModel
            {
                JudoId = judoId,
                YourConsumerReference = yourConsumerReference,
                Amount = 25,
                Wallet = new VisaCheckoutWalletModel
                {
                    CallId = callId,
                    EncryptedKey = encKey,
                    EncryptedPaymentData = encPaymentData
                }
            };
        }
    }
}
