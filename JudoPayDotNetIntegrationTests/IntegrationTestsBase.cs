using System;
using System.Net;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;

namespace JudoPayDotNetIntegrationTests
{
    public abstract class IntegrationTestsBase
    {
        protected JudoPayApi JudoPayApi;
        protected JudoPayApi JudoPayApiElevated;
        protected readonly Configuration Configuration = new Configuration();

        protected IntegrationTestsBase() 
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            JudoPayApi = JudoPaymentsFactory.Create(Configuration.JudoEnvironment, Configuration.Token, Configuration.Secret);
            JudoPayApiElevated = JudoPaymentsFactory.Create(Configuration.JudoEnvironment, Configuration.ElevatedPrivilegesToken, Configuration.ElevatedPrivilegesSecret);
        }

        protected CardPaymentModel GetCardPaymentModel(string yourConsumerReference = null, string cardNumber = "4976000000003436", string cv2 = "452")
        {
            if (string.IsNullOrEmpty(yourConsumerReference))
                yourConsumerReference = Guid.NewGuid().ToString();

            return new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourConsumerReference = yourConsumerReference,
                Amount = 25,
                CardNumber = cardNumber,
                CV2 = cv2,
                ExpiryDate = "12/20",
                CardAddress = new CardAddressModel
                {
                    Line1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                }
            };
        }

        protected TokenPaymentModel GetTokenPaymentModel(string cardToken, string yourConsumerReference = null, decimal amount = 25)
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
                ConsumerToken = "ABSE"
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
    }
}
