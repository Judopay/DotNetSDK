using System;
using System.Net;
using JudoPayDotNet;
using JudoPayDotNet.Models;

namespace JudoPayDotNetIntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        protected JudoPayApi JudoPayApi;
        protected JudoPayApi JudoPayApiElevated;
        protected readonly Configuration Configuration = new Configuration();

        protected IntegrationTestsBase() 
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            JudoPayApi = JudoPaymentsFactory.Create(Configuration.JudoEnvironment, Configuration.Token, Configuration.Secret);
            JudoPayApiElevated = JudoPaymentsFactory.Create(Configuration.JudoEnvironment, Configuration.ElevatedPrivilegesToken, Configuration.ElevatedPrivilegesSecret);
        }

        protected CardPaymentModel GetCardPaymentModel(string yourConsumerReference = null, 
                                                            string cardNumber = "4976000000003436", 
                                                            string cv2 = "452", 
                                                            string postCode = "TR14 8PA", 
                                                            bool? recurringPayment = null, 
                                                            string judoId = null)
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
                yourConsumerReference = Guid.NewGuid().ToString();

            return new CardPaymentModel
            {
                JudoId = judoId ?? Configuration.Judoid,
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

        protected TokenPaymentModel GetTokenPaymentModel(string cardToken, string yourConsumerReference = null, decimal amount = 25, bool? recurringPayment = null)
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
                yourConsumerReference = Guid.NewGuid().ToString();

            return new TokenPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                Amount = amount,
                CardToken = cardToken,
                CV2 = "452",
                ConsumerToken = "ABSE",
                RecurringPayment = recurringPayment
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

        protected VisaCheckoutPaymentModel GetVisaCheckoutPaymentModel(string callId, string encKey, string encPaymentData, string yourConsumerReference = "Consumer1")
        {
            return new VisaCheckoutPaymentModel
            {
                JudoId = Configuration.Judoid,
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

        protected JudoPayApi UseCybersourceGateway()
        {
            return JudoPaymentsFactory.Create(Configuration.JudoEnvironment, Configuration.Cybersource_Token, Configuration.Cybersource_Secret);
        }
    }
}
