using FluentValidation;


namespace JudoPayDotNet.Models.Validations
{
    internal class PKPaymentValidator: AbstractValidator<ApplePayPaymentModel>
	{
        public PKPaymentValidator()
		{
			RuleFor(model => model.PkPayment)
				.NotEmpty().WithMessage("You must supply your Apple Pay token");
		}
	}
}
