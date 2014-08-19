using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class WebPaymentRequestModelValidator : AbstractValidator<WebPaymentRequestModel>
    {
        public WebPaymentRequestModelValidator()
        {
            RuleFor(model => model.JudoId)
                .NotEmpty().WithMessage("Judo Id must be supplied.");

            RuleFor(model => model.YourConsumerReference)
                    .NotEmpty().WithMessage("You must supply your unique consumer reference")
                    .Length(1, 50).WithMessage("You unique consumer reference needs to be less than 50 characters.");

            RuleFor(model => model.YourPaymentReference)
                .NotEmpty().WithMessage("You must supply your unique payment reference")
                .Length(1, 50).WithMessage("You unique payment reference needs to be less than 50 characters.");

            RuleFor(model => model.Amount)
                .NotEmpty().WithMessage("You must supply the amount you wish to pay.")
                .GreaterThan(0.01M).WithMessage("Sorry, this payment amount is not valid.");
        }
    }
}
