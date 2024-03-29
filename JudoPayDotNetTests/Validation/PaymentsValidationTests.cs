﻿using FluentValidation.TestHelper;
using JudoPayDotNet.Models;
using JudoPayDotNet.Models.Validations;
using NUnit.Framework;

namespace JudoPayDotNetTests.Validation
{
    [TestFixture]
    public class PaymentsValidationTests
    {
        [Test]
        public void ValidatePkPaymentWithErrorOnSpecificCardPaymentInformation()
        {
            var payment = new ApplePayPaymentModel();

            var validator = new PKPaymentValidator();

            var result = validator.TestValidate(payment);

            result.ShouldHaveValidationErrorFor(model => model.PkPayment);
        }
    }
}
