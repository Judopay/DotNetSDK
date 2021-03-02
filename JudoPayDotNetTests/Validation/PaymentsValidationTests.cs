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
        public void ValidateCardPaymentWithErrorOnSpecificCardPaymentInformation()
        {
            var payment = new CardPaymentModel();

            var validator = new CardPaymentValidator();

            validator.ShouldHaveValidationErrorFor(model => model.CardNumber, payment);
        }

        [Test]
        public void ValidateCardPaymentWithErrorOnPaymentValidation()
        {
            var payment = new CardPaymentModel
            {
                Amount = 2.0m,
                CardAddress = new CardAddressModel
                {
                    Address1 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                },
                CardNumber = "348417606737499",
                ConsumerLocation = new ConsumerLocationModel
                {
                    Latitude = 40m,
                    Longitude = 14m
                },
                CV2 = "420",
                EmailAddress = "testaccount@judo.com",
                ExpiryDate = "12/25",
                MobileNumber = "07999999999",
                YourConsumerReference = "User10"
            };

            var validator = new CardPaymentValidator();

            validator.ShouldHaveValidationErrorFor(model => model.JudoId, payment);
        }

        [Test]
        public void ValidateCardPaymentWithoutErrors()
        {
            var payment = new CardPaymentModel
            {
                Amount = 2.0m,
                CardAddress = new CardAddressModel
                {
                    Address1 = "Test Street",
                    Address2 = "Test Street",
                    Address3 = "Test Street",
                    PostCode = "W40 9AU",
                    Town = "Town"
                },
                CardNumber = "348417606737499",
                ConsumerLocation = new ConsumerLocationModel
                {
                    Latitude = 40m,
                    Longitude = 14m
                },
                CV2 = "420",
                EmailAddress = "testaccount@judo.com",
                ExpiryDate = "12/25",
                JudoId = "12356",
                MobileNumber = "07999999999",
                PhoneCountryCode = "44",
                YourConsumerReference = "User10"
            };

            var validator = new CardPaymentValidator();

            var result = validator.Validate(payment);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.IsValid);
        }
    }
}
