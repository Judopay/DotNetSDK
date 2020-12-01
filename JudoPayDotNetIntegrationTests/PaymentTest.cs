﻿using System;
using System.Threading.Tasks;
using JudoPayDotNet.Enums;
using JudoPayDotNet.Models;
using NUnit.Framework;

namespace JudoPayDotNetIntegrationTests
{
    [TestFixture]
    public class PaymentTest : IntegrationTestsBase
    {
        [Test]
        public async Task ASimplePayment()
        {
            var paymentWithCard = GetCardPaymentModel();

            var response = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var receipt = response.Response as PaymentReceiptModel;
            Assert.That(receipt, Is.Not.Null);
            Assert.That(receipt.CardDetails, Is.Not.Null, "Missing carddetails property on receipt");
            Assert.That(receipt.CardDetails.CardLastfour, Is.Not.Empty);
            Assert.That(receipt.CardDetails.EndDate, Is.Not.Empty);
            Assert.That(receipt.CardDetails.CardScheme, Is.EqualTo("Visa"));
            Assert.That(receipt.CardDetails.CardFunding, Is.EqualTo("Debit"));
            Assert.That(receipt.CardDetails.CardCategory, Is.EqualTo("Classic"));
            Assert.That(receipt.CardDetails.CardCountry, Is.EqualTo("FR"));
            Assert.That(receipt.CardDetails.Bank, Is.EqualTo("Credit Industriel Et Commercial"));
            Assert.That(receipt.Message, Does.Match("AuthCode: \\d{6}"), $"Result message on receipt not in correct format AuthCode: xxxxxx. Was {receipt.Message}");
            Assert.That(receipt.MerchantName, Is.Not.Empty);
            Assert.That(receipt.AppearsOnStatementAs, Is.Not.Empty);
            Assert.That(receipt.CreatedAt, Is.Not.Null);
            Assert.That(receipt.NetAmount, Is.GreaterThan(0));
            Assert.That(receipt.Amount, Is.GreaterThan(0));
            Assert.That(receipt.Currency, Is.EqualTo("GBP"));

            Assert.That(receipt.Risks.PostcodeCheck, Is.EqualTo("PASSED"));
            Assert.That(receipt.Risks.MerchantSuggestion, Is.EqualTo("Allow"));
        }

        [Test]
        public async Task ARecurringPayment()
        {
            var initialPaymentWithCard = GetCardPaymentModel(initialRecurringPayment: true, judoId: Configuration.Cybersource_Judoid);

            var initialResponse = await JudoPayApiCyberSource.Payments.Create(initialPaymentWithCard);

            Assert.IsNotNull(initialResponse);
            Assert.IsFalse(initialResponse.HasError);
            Assert.AreEqual("Success", initialResponse.Response.Result);

            var initialPaymentReceipt = initialResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(initialPaymentReceipt);
            Assert.IsNull(initialPaymentReceipt.Recurring);

            var recurringPaymentWithCard = GetCardPaymentModel(initialRecurringPayment: false, recurringPayment: true,
                recurringPaymentType: RecurringPaymentType.Recurring, relatedReceiptId: "" + initialPaymentReceipt.ReceiptId,
                judoId: Configuration.Cybersource_Judoid);

            var recurringResponse = await JudoPayApiCyberSource.Payments.Create(recurringPaymentWithCard);
            Assert.IsNotNull(recurringResponse);
            Assert.IsFalse(recurringResponse.HasError);
            Assert.AreEqual("Success", recurringResponse.Response.Result);

            var recurringPaymentReceipt = recurringResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(recurringPaymentReceipt);
            Assert.IsTrue(recurringPaymentReceipt.Recurring);
        }

        [Test]
        public async Task AnMitPayment()
        {
            var initialPaymentWithCard = GetCardPaymentModel(initialRecurringPayment: true, judoId: Configuration.Cybersource_Judoid);

            var initialResponse = await JudoPayApiCyberSource.Payments.Create(initialPaymentWithCard);

            Assert.IsNotNull(initialResponse);
            Assert.IsFalse(initialResponse.HasError);
            Assert.AreEqual("Success", initialResponse.Response.Result);

            var initialPaymentReceipt = initialResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(initialPaymentReceipt);
            Assert.IsNull(initialPaymentReceipt.Recurring);

            var mitPaymentWithCard = GetCardPaymentModel(initialRecurringPayment: false, recurringPayment: true,
                recurringPaymentType: RecurringPaymentType.Mit, relatedReceiptId: "" + initialPaymentReceipt.ReceiptId,
                judoId: Configuration.Cybersource_Judoid);

            var mitResponse = await JudoPayApiCyberSource.Payments.Create(mitPaymentWithCard);
            Assert.IsNotNull(mitResponse);
            Assert.IsFalse(mitResponse.HasError);
            Assert.AreEqual("Success", mitResponse.Response.Result);

            var mitPaymentReceipt = mitResponse.Response as PaymentReceiptModel;
            Assert.IsNotNull(mitPaymentReceipt);
            Assert.IsTrue(mitPaymentReceipt.Recurring);
        }

        [Test]
        public async Task ATokenPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();
            var paymentWithCard = GetCardPaymentModel(consumerReference);

            var response = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);

            // Fetch the card token
            var cardToken = receipt.CardDetails.CardToken;

            var paymentWithToken = GetTokenPaymentModel(cardToken, consumerReference, 26);
            response = await JudoPayApiIridium.Payments.Create(paymentWithToken);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);

            var cardTokenReceipt = response.Response as PaymentReceiptModel;
            Assert.That(cardTokenReceipt, Is.Not.Null);
            Assert.That(cardTokenReceipt.CardDetails, Is.Not.Null, "Missing carddetails property on receipt");
            Assert.That(cardTokenReceipt.CardDetails.CardLastfour, Is.Not.Empty);
            Assert.That(cardTokenReceipt.CardDetails.EndDate, Is.Not.Empty);
            Assert.That(cardTokenReceipt.CardDetails.CardScheme, Is.EqualTo("Visa"));
            Assert.That(cardTokenReceipt.CardDetails.CardFunding, Is.EqualTo("Debit"));
            Assert.That(cardTokenReceipt.CardDetails.CardCategory, Is.EqualTo("Classic"));
            Assert.That(cardTokenReceipt.CardDetails.CardCountry, Is.EqualTo("FR"));
            Assert.That(cardTokenReceipt.CardDetails.Bank, Is.EqualTo("Credit Industriel Et Commercial"));
            Assert.That(cardTokenReceipt.Message, Does.Match("AuthCode: \\d{6}"), $"Result message on receipt not in correct format AuthCode: xxxxxx. Was {cardTokenReceipt.Message}");
            Assert.That(cardTokenReceipt.MerchantName, Is.Not.Empty);
            Assert.That(cardTokenReceipt.AppearsOnStatementAs, Is.Not.Empty);
            Assert.That(cardTokenReceipt.CreatedAt, Is.Not.Null);
            Assert.That(cardTokenReceipt.NetAmount, Is.GreaterThan(0));
            Assert.That(cardTokenReceipt.Amount, Is.GreaterThan(0));
            Assert.That(cardTokenReceipt.Currency, Is.EqualTo("GBP"));

            Assert.That(receipt.Risks.PostcodeCheck, Is.EqualTo("PASSED"));
            Assert.That(receipt.Risks.MerchantSuggestion, Is.EqualTo("Allow"));
        }

        [Test]
        public async Task ATokenRecurringPayment()
        {
            var consumerReference = Guid.NewGuid().ToString();

            var paymentWithCard = GetCardPaymentModel(consumerReference, recurringPayment: true, judoId: Configuration.Cybersource_Judoid);

            var response = await JudoPayApiCyberSource.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);

            Assert.AreEqual("Success", receipt.Result);
            var paymentReceipt = response.Response as PaymentReceiptModel;
            Assert.IsInstanceOf<PaymentReceiptModel>(response.Response);
            Assert.IsTrue(paymentReceipt.Recurring);
        }

        [Test]
        public async Task AOneTimePayment()
        {
            var oneTimePaymentModel = GetOneTimePaymentModel().Result;

            var response = await JudoPayApiIridium.Payments.Create(oneTimePaymentModel);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);

            var receipt = response.Response as PaymentReceiptModel;

            Assert.IsNotNull(receipt);
            Assert.AreEqual("Success", receipt.Result);
        }

        [Test]
        public async Task ADeclinedCardPayment()
        {
            var paymentWithCard = GetCardPaymentModel("432438862", "4221690000004963", "125");

            var response = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Declined", response.Response.Result);
        }

        [Test]
        public async Task DeDuplicationTest()
        {
            var paymentWithCard = GetCardPaymentModel("432438862");

            var response1 = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            var response2 = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.AreEqual(response1.Response.ReceiptId, response2.Response.ReceiptId);
        }
        
        [Test]
        public async Task PrimaryAccountDetailsPayment()
        {
            var paymentWithCard = GetPrimaryAccountPaymentModel();

            var response = await JudoPayApiIridium.Payments.Create(paymentWithCard);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.HasError);
            Assert.AreEqual("Success", response.Response.Result);}
    }
}