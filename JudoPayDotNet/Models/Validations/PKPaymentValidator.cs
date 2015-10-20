using FluentValidation;


namespace JudoPayDotNet.Models.Validations
{
    internal class PKPaymentValidator: PaymentsBaseValidator<PKPaymentModel>
	{
        public PKPaymentValidator()
		{
			RuleFor(model => model.CardNumber)
				.NotEmpty().WithMessage("You must supply your consumer token");

			RuleFor(model => model.ExpiryDate)
				.NotEmpty().WithMessage("You must supply your card token");
		}
	}
}
