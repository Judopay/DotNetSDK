using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JudoPayDotNet;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Http;
using JudoPayDotNet.Models;
using Newtonsoft.Json;

namespace JudoPayDotNetIntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        protected JudoPayApi JudoPayApiIridium;
        protected JudoPayApi JudoPayApiCyberSource;
        protected JudoPayApi JudoPayApiSafeCharge;
        protected JudoPayApi JudoPayApiElevated;
        protected readonly Configuration Configuration = new Configuration();

        protected IntegrationTestsBase() 
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            JudoPayApiIridium = JudoPaymentsFactory.Create(Configuration.JudoEnvironment, Configuration.Token, Configuration.Secret);
            JudoPayApiCyberSource = JudoPaymentsFactory.Create(Configuration.JudoEnvironment, Configuration.Cybersource_Token, Configuration.Cybersource_Secret);
            JudoPayApiSafeCharge = JudoPaymentsFactory.Create(Configuration.JudoEnvironment, Configuration.SafeCharge_Token, Configuration.SafeCharge_Secret);
            JudoPayApiElevated = JudoPaymentsFactory.Create(Configuration.JudoEnvironment, Configuration.ElevatedPrivilegesToken, Configuration.ElevatedPrivilegesSecret);

        }

        protected CardPaymentModel GetCardPaymentModel(
            string yourConsumerReference = null,
            string cardNumber = "4976000000003436",
            string cv2 = "452",
            string postCode = "TR14 8PA",
            bool? initialRecurringPayment = null,
            bool? recurringPayment = null,
            string relatedReceiptId = null,
            RecurringPaymentType? recurringPaymentType = null,

            string judoId = null)
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            var ret = new CardPaymentModel
            {
                JudoId = judoId ?? Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                Amount = 25,
                CardNumber = cardNumber,
                CV2 = cv2,
                ExpiryDate = "12/25",
                CardAddress = new CardAddressModel
                {
                    Address1 = "32 Edward Street",
                    PostCode = postCode,
                    Town = "Camborne"
                }
            };
            if (initialRecurringPayment != null)
            {
                ret.InitialRecurringPayment = initialRecurringPayment;
            }
            if (recurringPayment != null)
            {
                ret.RecurringPayment = recurringPayment;
            }
            if (relatedReceiptId != null)
            {
                ret.RelatedReceiptId = relatedReceiptId;
            }
            if (recurringPaymentType != null)
            {
                ret.RecurringPaymentType = recurringPaymentType;
            }
            return ret;
        }

        protected TokenPaymentModel GetTokenPaymentModel(
            string cardToken, 
            string yourConsumerReference = null, 
            decimal amount = 25,
            bool? recurringPayment = null, 
            string judoId = null)
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            return new TokenPaymentModel
            {
                JudoId = judoId ?? Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                Amount = amount,
                CardToken = cardToken,
                CV2 = "452",
                ConsumerToken = "ABSE",
                RecurringPayment = recurringPayment
            };
        }
        
        protected SaveCardModel GetSaveCardModel(
            string yourConsumerReference = null,
            string cardNumber = "4976000000003436",
            string postCode = "TR14 8PA",
            bool? recurringPayment = null,
            string judoId = null)
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            return new SaveCardModel
            {
                YourConsumerReference = yourConsumerReference,
                CardNumber = cardNumber,
                ExpiryDate = "12/25",
                CardAddress = new CardAddressModel
                {
                    Address1 = "32 Edward Street",
                    PostCode = postCode,
                    Town = "Camborne"
                }
            };
        }

        protected async Task<SaveEncryptedCardModel> GetSaveEncryptedCardModel(
            string judoId = null,
            string yourConsumerReference = null)
        {
            var oneUseTokenModel = await GetOneUseToken();

            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            return new SaveEncryptedCardModel
            {
                OneUseToken = oneUseTokenModel.OneUseToken,
                JudoId = Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                CardAddress = new CardAddressModel
                {
                    Address1 = "32 Edward Street",
                    PostCode = "TR14 8PA",
                    Town = "Camborne"
                }
            };
        }
        
        protected RegisterCardModel GetRegisterCardModel(
            string yourConsumerReference = null,
            string cardNumber = "4976000000003436",
            string cv2 = "452",
            string postCode = "TR14 8PA",
            bool? recurringPayment = null,
            string judoId = null)
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            return new RegisterCardModel
            {
                YourConsumerReference = yourConsumerReference,
                CardNumber = cardNumber,
                CV2 = cv2,
                ExpiryDate = "12/25",
                CardAddress = new CardAddressModel
                {
                    Address1 = "32 Edward Street",
                    PostCode = postCode,
                    Town = "Camborne"
                }
            };
        }

        protected async Task<RegisterEncryptedCardModel> GetRegisterEncryptedCardModel(
            string judoId = null,
            string yourConsumerReference = null)
        {
            var oneUseTokenModel = await GetOneUseToken();

            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            return new RegisterEncryptedCardModel
            {
                OneUseToken = oneUseTokenModel.OneUseToken,
                JudoId = Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                CardAddress = new CardAddressModel
                {
                    Address1 = "32 Edward Street",
                    PostCode = "TR14 8PA",
                    Town = "Camborne"
                }
            };
        }

        protected CheckCardModel GetCheckCardModel(
            string judoId = null, 
            string cardNumber = "4976000000003436", 
            string cv2 = "452")
        {
            return new CheckCardModel
            {
                JudoId = judoId ?? Configuration.Judoid,
                YourConsumerReference = Guid.NewGuid().ToString(),
                CardNumber = cardNumber,
                CV2 = cv2,
                ExpiryDate = "12/25",
                CardAddress = new CardAddressModel
                {
                    Address1 = "32 Edward Street",
                    PostCode = "TR14 8PA",
                    Town = "Camborne"
                }
            };
        }

        protected async Task<CheckEncryptedCardModel> GetCheckEncryptedCardModel(
            string judoId = null,
            string yourConsumerReference = null)
        {
            var oneUseTokenModel = await GetOneUseToken(true);

            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            return new CheckEncryptedCardModel
            {
                OneUseToken = oneUseTokenModel.OneUseToken,
                JudoId = judoId ?? Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                CardAddress = new CardAddressModel
                {
                    Address1 = "32 Edward Street",
                    PostCode = "TR14 8PA",
                    Town = "Camborne"
                }
            };
        }

        protected async Task<OneTimePaymentModel> GetOneTimePaymentModel()
        {
            var oneUseTokenModel = await GetOneUseToken();

            return new OneTimePaymentModel
            {
                OneUseToken = oneUseTokenModel.OneUseToken,
                JudoId = Configuration.Judoid,
                YourConsumerReference = Guid.NewGuid().ToString(),
                Amount = 25,
                CardAddress = new CardAddressModel
                {
                    Address1 = "32 Edward Street",
                    PostCode = "TR14 8PA",
                    Town = "Camborne"
                }
            };
        }
        
        private async Task<OneUseTokenModel> GetOneUseToken(bool getAlt = false)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Api-Version", VersioningHandler.DEFAULT_API_VERSION);
            client.DefaultRequestHeaders.Add("Authorization", getAlt ? $"Simple {Configuration.Cybersource_Token}" : $"Simple {Configuration.Token}");

            var cardDetailsModel = new Dictionary<string, string>
            {
                {"cardNumber", "4976000000003436"},
                {"expiryDate", "12/25"},
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

        protected WebPaymentRequestModel GetWebPaymentRequestModel()
        {
            return new WebPaymentRequestModel
            {
                Amount = 10,
                CardAddress = new WebPaymentCardAddress
                {
                    CardHolderName = "Test User",
                    Address1 = "Test Street",
                    Address2 = "Test Street",
                    Address3 = "Test Street",
                    Town = "London",
                    PostCode = "W31 4HS",
                    CountryCode = 826
                },
                ClientIpAddress = "127.0.0.1",
                CompanyName = "Test",
                Currency = "GBP",
                ExpiryDate = DateTimeOffset.Now.AddMinutes(30),
                JudoId = Configuration.Judoid,
                PartnerServiceFee = 10,
                PaymentCancelUrl = "http://test.com",
                PaymentSuccessUrl = "http://test.com",
                Reference = "42421",
                Status = WebPaymentStatus.Open,
                TransactionType = TransactionType.PAYMENT,
                YourConsumerReference = "4235325"
            };
        }

        protected CardPaymentModel GetPrimaryAccountPaymentModel(string yourConsumerReference = null)
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            PrimaryAccountDetailsModel accountDetails = new PrimaryAccountDetailsModel
            {
                Name = "Judo Pay",
                AccountNumber = "1234567",
                DateOfBirth = "2000-12-31",
                PostCode = "EC2A 4DP"
            };

            if (string.IsNullOrEmpty(yourConsumerReference))
            {
                yourConsumerReference = Guid.NewGuid().ToString();
            }

            CardPaymentModel paymentModel = GetCardPaymentModel(yourConsumerReference);
            
            paymentModel.PrimaryAccountDetails = accountDetails;

            return paymentModel;
        }


    }
}
