using System;
using System.Linq;
using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
    public class PaymentsBaseValidator<TPaymentModel> : AbstractValidator<TPaymentModel> where TPaymentModel : PaymentModel
    {
        public PaymentsBaseValidator()
        {
            
            RuleFor(model => model.JudoId)
                .NotEmpty().WithMessage("Judo Id must be supplied.");

            RuleFor(model => model.Amount)
                 .NotEmpty().WithMessage("You must supply the amount you wish to pay.")
                 .InclusiveBetween(0.01M, 5000M).WithMessage("Sorry, this payment amount is not valid.");

            RuleFor(model => model.Amount).Must(amount =>
            {
                var countOfDecimalPlaces = BitConverter.GetBytes(decimal.GetBits(amount)[3])[2];
                return countOfDecimalPlaces <= 2;
            }).WithMessage("the amount submitted has more than two decimal places. Please round before submitting the transaction.");

            RuleFor(model => model.PartnerServiceFee)
                 .InclusiveBetween(0.00M, 5000M).WithMessage("Sorry, this partner service fee amount is not valid.");

            RuleFor(model => model.YourConsumerReference)
                .NotEmpty().WithMessage("You must supply your unique consumer reference")
                .Length(1, 50).WithMessage("You unique consumer reference needs to be less than 50 characters."); ;

            RuleFor(model => model.YourPaymentReference)
                .NotEmpty().WithMessage("You must supply your unique payment reference")
                .Length(1, 50).WithMessage("You unique payment reference needs to be less than 50 characters.");

            RuleFor(model => model.Currency)
                .NotEmpty().WithMessage("You must supply your currency")
                .Length(3).WithMessage("You currency needs to be 3 characters.")
                .Equal("GBP").WithMessage("Sorry, we only support GBP currently");

            string[] validDeviceCategories = new[] { "mobile", "desktop" };

            When(m => !String.IsNullOrWhiteSpace(m.DeviceCategory),
                () => RuleFor(model => model.DeviceCategory)
                    .Cascade(CascadeMode.StopOnFirstFailure)
                    .Must(v => validDeviceCategories.Any(value => String.Compare(value, v, StringComparison.OrdinalIgnoreCase) == 0)).WithMessage("DeviceCategory is unknown, valid values mobile or desktop")
                    );
        }
    }

    public class PaymentValidator : PaymentsBaseValidator<PaymentModel>
    {
        public PaymentValidator()
        {
            
        }
    }

    public class CardPaymentValidator : PaymentsBaseValidator<CardPaymentModel>
    {
        public CardPaymentValidator()
        {
            RuleFor(model => model.CardNumber)
                .NotEmpty().WithMessage("You must supply your card number");

            RuleFor(model => model.ExpiryDate)
                .NotEmpty().WithMessage("You must supply your expiry date");

            RuleFor(model => model.CardAddress)
                .NotEmpty().WithMessage("You must supply your card address")
                .SetValidator(new CardAddressValidator());
        }
    }

    public class TokenPaymentValidator : PaymentsBaseValidator<TokenPaymentModel>
    {
        public TokenPaymentValidator()
        {
            RuleFor(model => model.ConsumerToken)
                .NotEmpty().WithMessage("You must supply your consumer token");

            RuleFor(model => model.CardToken)
                .NotEmpty().WithMessage("You must supply your card token");
        }
    }

    public class CardAddressValidator : AbstractValidator<CardAddressModel>
    {
        public CardAddressValidator()
        {
            RuleFor(model => model.Line1)
                .NotEmpty().WithMessage("You must supply address line 1");

            //RuleFor(model => model.Line2)
            //    .NotEmpty().WithMessage("You must supply address line 2");

            //RuleFor(model => model.Line3)
            //    .NotEmpty().WithMessage("You must supply address line 3");

            RuleFor(model => model.Town)
                .NotEmpty().WithMessage("You must supply address town");

            RuleFor(model => model.PostCode)
                .NotEmpty().WithMessage("You must supply address post code");
        }
    }
}
