using System;
using System.Net;
using JudoPayDotNet;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;

namespace JudoPayDotNetIntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        protected JudoPayApi JudoPayApiIridium;
        protected JudoPayApi JudoPayApiBase;
        protected JudoPayApi JudoPayApiElevated;
        protected JudoPayApi JudoPayApiThreeDSecure2;
        protected readonly Configuration Configuration = new Configuration();

        protected IntegrationTestsBase() 
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            JudoPayApiBase = JudoPayApiIridium = JudoPaymentsFactory.Create(Configuration.JudoEnvironment,
                Configuration.Token, Configuration.Secret);
            JudoPayApiElevated = JudoPaymentsFactory.Create(Configuration.JudoEnvironment,
                Configuration.ElevatedPrivilegesToken, Configuration.ElevatedPrivilegesSecret);
            JudoPayApiThreeDSecure2 = JudoPaymentsFactory.Create(Configuration.JudoEnvironment,
                Configuration.ThreeDSecure2Token, Configuration.ThreeDSecure2Secret);
        }

        protected CardPaymentModel GetCardPaymentModel(
            string yourConsumerReference = null,
            string cardNumber = "4976000000003436",
            string cv2 = "452",
            string postCode = "TR14 8PA",
            string cardHolderName = "John Smith",
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
                Currency  = "GBP",
                CardNumber = cardNumber,
                CV2 = cv2,
                ExpiryDate = "12/25",
                CardHolderName = cardHolderName,
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

        protected CardPaymentModel GetCardPaymentNoCardAddressModel(
            string yourConsumerReference = null,
            string cardNumber = "4976000000003436",
            string cv2 = "452",
            string cardHolderName = "John Smith",
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
                CardHolderName = cardHolderName,
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
                JudoId = judoId,
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

        protected CheckCardModel GetCheckCardModel(
            string judoId = null, 
            string cardNumber = "4976000000003436", 
            string cv2 = "452",
            string currency = null)
        {
            return new CheckCardModel
            {
                JudoId = judoId ?? Configuration.Judoid,
                YourConsumerReference = Guid.NewGuid().ToString(),
                CardNumber = cardNumber,
                CV2 = cv2,
                ExpiryDate = "12/25",
                Currency = currency ?? "GBP",
                CardAddress = new CardAddressModel
                {
                    Address1 = "32 Edward Street",
                    PostCode = "TR14 8PA",
                    Town = "Camborne"
                }
            };
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
                CancelUrl = "https://www.test.com",
                SuccessUrl = "https://www.test.com",
                Reference = "42421",
                Status = WebPaymentStatus.Open,
                TransactionType = TransactionType.PAYMENT,
                YourConsumerReference = "4235325"
            };
        }

        protected WebPaymentRequestModel Get3ds2WebPaymentRequestModel()
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
                CancelUrl = "https://www.test.com",
                SuccessUrl = "https://www.test.com",
                Reference = "42421",
                Status = WebPaymentStatus.Open,
                TransactionType = TransactionType.PAYMENT,
                YourConsumerReference = "4235325",
                MobileNumber = "07999999999",
                PhoneCountryCode = "44",
                EmailAddress = "test@judopay.com",
                ThreeDSecure = new ThreeDSecureTwoModel()
                {
                    ChallengeRequestIndicator = ThreeDSecureTwoChallengeRequestIndicator.ChallengePreferred,
                    ScaExemption = ThreeDSecureTwoScaExemption.TransactionRiskAnalysis,
                    ChallengeNotificationUrl = "https://www.judopay.com",
                    MethodNotificationUrl = "https://www.judopay.com",
                    AuthenticationSource = ThreeDSecureTwoAuthenticationSource.Browser
                }
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
