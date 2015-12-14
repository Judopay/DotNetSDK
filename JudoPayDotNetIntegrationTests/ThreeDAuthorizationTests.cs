using System;
using System.Collections.Generic;
using System.Net.Http;
using HtmlAgilityPack;
using JudoPayDotNet;
using JudoPayDotNet.Models;
using JudoPayDotNetDotNet;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class ThreeDAuthorizationTests
    {
        private JudoPayApi _judo;

        [TestFixtureSetUp]
        public void SetupOnce()
        {
            _judo = JudoPaymentsFactory.Create(Configuration.Token,
                Configuration.Secret,
                Configuration.Baseaddress);
        }

        [Test]
        public void PaymentWithThreedSecure()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = Configuration.Judoid,
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976350000006891",
                CardAddress = new CardAddressModel
                {
                    Line1 = "242 Acklam Road",
                    Line2 = "Westbourne Park",
                    Line3 = "",
                    Town = "London",
                    PostCode = "W105JJ"
                },
                CV2 = "341",
                ExpiryDate = "12/15",
                MobileNumber = "07123456789",
                EmailAddress = "test@gmail.com",
                UserAgent = "Mozilla/5.0,(Windows NT 6.1; WOW64),AppleWebKit/537.36,(KHTML, like Gecko),Chrome/33.0.1750.154,Safari/537.36",
                AcceptHeaders = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
                DeviceCategory = "Mobile"
            };
        

            var response = _judo.Payments.Create(paymentWithCard).Result;

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentRequiresThreeDSecureModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Requires 3D Secure", receipt.Result);
            Assert.IsNotEmpty(receipt.Md);
        }

        [Test]
        public void FullPaymentWithThreedSecure()
        {
            var paymentWithCard = new CardPaymentModel
            {
                JudoId = "100016",
                YourPaymentReference = "578543",
                YourConsumerReference = "432438862",
                Amount = 25,
                CardNumber = "4976350000006891",
                CardAddress = new CardAddressModel
                {
                    Line1 = "242 Acklam Road",
                    Line2 = "Westbourne Park",
                    Line3 = "",
                    Town = "London",
                    PostCode = "W105JJ"
                },
                CV2 = "341",
                ExpiryDate = "12/15",
                MobileNumber = "07123456789",
                EmailAddress = "test@gmail.com",
                UserAgent = "Mozilla/5.0,(Windows NT 6.1; WOW64),AppleWebKit/537.36,(KHTML, like Gecko),Chrome/33.0.1750.154,Safari/537.36",
                AcceptHeaders = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
                DeviceCategory = "Mobile"
            };

            var response = _judo.Payments.Create(paymentWithCard).Result;

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

                var threeDResult = _judo.ThreeDs.Complete3DSecure(receipt.ReceiptId, new ThreeDResultModel {PaRes = paResValue}).Result;

                Assert.IsNotNull(threeDResult);
                Assert.IsFalse(threeDResult.HasError);
                Assert.AreEqual("Success", threeDResult.Response.Result);

            });

            taskSendMDandPaReqToAcsServer.Wait();
        }
    }
}
