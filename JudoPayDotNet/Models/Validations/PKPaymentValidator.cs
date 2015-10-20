using FluentValidation;


namespace JudoPayDotNet.Models.Validations
{
    internal class PKPaymentValidator: PaymentsBaseValidator<PKPaymentModel>
	{
        public PKPaymentValidator()
		{
			RuleFor(model => model.Token)
				.NotEmpty().WithMessage("You must supply your Apple Pay token");

			RuleFor(model => model.BillingAddress)
				.NotEmpty().WithMessage("You must supply your Billing adress");
		}
	}
}
