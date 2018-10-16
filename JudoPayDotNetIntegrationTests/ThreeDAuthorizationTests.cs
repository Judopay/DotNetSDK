using System.Collections.Generic;
using System.Net.Http;
using HtmlAgilityPack;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class ThreeDAuthorizationTests : IntegrationTestsBase
    {
        private static string[] _testCases =
        {
            "iridium",
            "cybersource"
        };

        [Test]
        [TestCaseSource(nameof(_testCases))]
        public void PaymentWithThreedSecure(string gatewayName)
        {
            var judoId = GetJudoId(gatewayName);
            var client = GetClient(gatewayName);

            // Different card depending on the gateway
            var paymentWithCard = GetCardPaymentModelForGateway(gatewayName);
            paymentWithCard.YourConsumerReference = "432438862";
            
            paymentWithCard.MobileNumber = "07123456789";
            paymentWithCard.EmailAddress = "test@gmail.com";
            paymentWithCard.UserAgent = "Mozilla/5.0,(Windows NT 6.1; WOW64),AppleWebKit/537.36,(KHTML, like Gecko),Chrome/33.0.1750.154,Safari/537.36";
            paymentWithCard.AcceptHeaders = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            paymentWithCard.DeviceCategory = "Mobile";

            var response = client.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentRequiresThreeDSecureModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Requires 3D Secure", receipt.Result);
            Assert.IsNotEmpty(receipt.Md);
        }

        [Test]
        [TestCaseSource(nameof(_testCases))]
        public void FullPaymentWithThreedSecure(string gatewayName)
        {
            var judoId = GetJudoId(gatewayName);
            var client = GetClient(gatewayName);

            var paymentWithCard = GetCardPaymentModel(
                judoId,
                yourConsumerReference: "432438862",
                cardNumber: "4976350000006891", 
                cv2: "341",
                postCode: "B42 1SX"
            );

            paymentWithCard.MobileNumber = "07123456789";
            paymentWithCard.EmailAddress = "test@gmail.com";
            paymentWithCard.UserAgent = "Mozilla/5.0,(Windows NT 6.1; WOW64),AppleWebKit/537.36,(KHTML, like Gecko),Chrome/33.0.1750.154,Safari/537.36";
            paymentWithCard.AcceptHeaders = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            paymentWithCard.DeviceCategory = "Mobile";
            
            var response = client.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentRequiresThreeDSecureModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Requires 3D Secure", receipt.Result);
            Assert.IsNotEmpty(receipt.Md);

            var httpClient = new HttpClient();
            var formContent = new FormUrlEncodedContent(new[] 
            {
                new KeyValuePair<string, string>("MD", receipt.Md),
				new KeyValuePair<string, string>("PaReq", receipt.PaReq),
				new KeyValuePair<string, string>("TermUrl", "https://pay.judopay.com/")
            });

            var taskSendMDandPaReqToAcsServer = httpClient.PostAsync(receipt.AcsUrl, formContent).ContinueWith(authResponse =>
            {
                var resultBody = authResponse.Result.Content.ReadAsStringAsync().Result;

                /* ok this next bit is a hack. I know on Iridium's ACS simulator the PaRes value is lurking in the HTML (It's a simulator after all!) */
                var doc = new HtmlDocument();
                doc.LoadHtml(resultBody);

                var formField = doc.DocumentNode.SelectSingleNode("//input[@name='PaRes']");

                var paResValue = formField.GetAttributeValue("value", "");

                Assert.That(paResValue, Is.Not.Empty);

                var threeDResult = client.ThreeDs.Complete3DSecure(receipt.ReceiptId, new ThreeDResultModel { PaRes = paResValue }).Result;

                Assert.IsNotNull(threeDResult);
                Assert.IsFalse(threeDResult.HasError);
                Assert.AreEqual("Success", threeDResult.Response.Result);
            });

            taskSendMDandPaReqToAcsServer.Wait();
        }
    }
}
