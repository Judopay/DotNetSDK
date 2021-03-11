using FluentValidation.TestHelper;
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
            var payment = new PKPaymentModel();

            var validator = new PKPaymentValidator();

            validator.ShouldHaveValidationErrorFor(model => model.PkPayment, payment);
        }
    }
}
