using System;
using System.Linq;
using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class PaymentsBaseValidator<TPaymentModel> : AbstractValidator<TPaymentModel> where TPaymentModel : PaymentModel
    {
        protected PaymentsBaseValidator()
        {
            RuleFor(model => model.JudoId)
                .NotEmpty()
                .WithErrorCode(((int)JudoModelErrorCode.JudoId_Not_Supplied).ToString())
                .WithMessage("Judo Id must be supplied.");

            RuleFor(model => model.Amount)
                 .NotEmpty()
                 .WithErrorCode(((int)JudoModelErrorCode.Amount_Not_Valid).ToString())
                 .WithMessage("You must supply the amount you wish to pay.")
                 .GreaterThanOrEqualTo(0.0M)
                 .WithErrorCode(((int)JudoModelErrorCode.Amount_Greater_Than_0).ToString())
                 .WithMessage("Sorry, this payment amount is not valid.");

            RuleFor(model => model.Amount).Must(amount =>
            {
                var countOfDecimalPlaces = BitConverter.GetBytes(decimal.GetBits(amount)[3])[2];
                return countOfDecimalPlaces <= 2;
            })
                .WithErrorCode(((int) JudoModelErrorCode.Amount_Decimal_Places).ToString())
                .WithMessage("the amount submitted has more than two decimal places. Please round before submitting the transaction.");

            RuleFor(model => model.PartnerServiceFee)
                 .GreaterThanOrEqualTo(0.00M)
                 .WithErrorCode(((int)JudoModelErrorCode.Partner_Service_Fee_Not_Valid).ToString())
                 .WithMessage("Sorry, this partner service fee amount is not valid.");

            RuleFor(model => model.YourConsumerReference)
                .NotEmpty()
                .WithErrorCode(((int)JudoModelErrorCode.Consumer_Reference_Not_Supplied).ToString())
                .WithMessage("You must supply your unique consumer reference")
                .Length(1, 50)
                .WithErrorCode(((int)JudoModelErrorCode.Consumer_Reference_Length).ToString())
                .WithMessage("You unique consumer reference needs to be less than 50 characters.");

            RuleFor(model => model.YourPaymentReference)
                .NotEmpty()
                .WithErrorCode(((int)JudoModelErrorCode.Payment_Reference_Not_Supplied).ToString())
                .WithMessage("You must supply your unique payment reference")
                .Length(1, 50)
                .WithErrorCode(((int)JudoModelErrorCode.Payment_Reference_Length).ToString())
                .WithMessage("You unique payment reference needs to be less than 50 characters.");

            RuleFor(model => model.Currency)
                .NotEmpty()
                .WithErrorCode(((int)JudoModelErrorCode.Currency_Required).ToString())
                .WithMessage("You must supply your currency")
                .Length(3)
                .WithErrorCode(((int)JudoModelErrorCode.Currency_Length).ToString())
                .WithMessage("You currency needs to be 3 characters.");

            var validDeviceCategories = new[] { "mobile", "desktop" };

            When(m => !string.IsNullOrWhiteSpace(m.DeviceCategory),
                () => RuleFor(model => model.DeviceCategory)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .Must(v => validDeviceCategories.Any(value => string.Compare(value, v, StringComparison.OrdinalIgnoreCase) == 0))
                    .WithErrorCode(((int)JudoModelErrorCode.Device_Category_Unknown).ToString())
                    .WithMessage("DeviceCategory is unknown, valid values mobile or desktop")
                );
        }
    }
}
