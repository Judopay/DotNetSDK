using System;
using System.Linq;
using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
    public class PaymentsBaseValidator<TPaymentModel> : AbstractValidator<TPaymentModel> where TPaymentModel : PaymentModel
    {
        protected PaymentsBaseValidator()
        {
            
            RuleFor(model => model.JudoId)
                .NotEmpty().WithMessage("Judo Id must be supplied.");

            RuleFor(model => model.Amount)
                 .NotEmpty().WithMessage("You must supply the amount you wish to pay.")
                 .GreaterThanOrEqualTo(0.01M).WithMessage("Sorry, this payment amount is not valid.");

            RuleFor(model => model.Amount).Must(amount =>
            {
                var countOfDecimalPlaces = BitConverter.GetBytes(decimal.GetBits(amount)[3])[2];
                return countOfDecimalPlaces <= 2;
            }).WithMessage("the amount submitted has more than two decimal places. Please round before submitting the transaction.");

            RuleFor(model => model.PartnerServiceFee)
                 .GreaterThanOrEqualTo(0.00M).WithMessage("Sorry, this partner service fee amount is not valid.");

            RuleFor(model => model.YourConsumerReference)
                .NotEmpty().WithMessage("You must supply your unique consumer reference")
                .Length(1, 50).WithMessage("You unique consumer reference needs to be less than 50 characters.");

            RuleFor(model => model.YourPaymentReference)
                .NotEmpty().WithMessage("You must supply your unique payment reference")
                .Length(1, 50).WithMessage("You unique payment reference needs to be less than 50 characters.");

            RuleFor(model => model.Currency)
                .NotEmpty().WithMessage("You must supply your currency")
                .Length(3).WithMessage("You currency needs to be 3 characters.");

            var validDeviceCategories = new[] { "mobile", "desktop" };

            When(m => !String.IsNullOrWhiteSpace(m.DeviceCategory),
                () => RuleFor(model => model.DeviceCategory)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .Must(v => validDeviceCategories.Any(value => String.Compare(value, v, StringComparison.OrdinalIgnoreCase) == 0)).WithMessage("DeviceCategory is unknown, valid values mobile or desktop")
                    );
        }
    }
}
