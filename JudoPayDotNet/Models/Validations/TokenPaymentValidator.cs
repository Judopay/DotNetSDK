using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class TokenPaymentValidator : PaymentsBaseValidator<TokenPaymentModel>
	{
		public TokenPaymentValidator()
		{
			RuleFor(model => model.ConsumerToken)
				.NotEmpty().WithMessage("You must supply your consumer token");

			RuleFor(model => model.CardToken)
				.NotEmpty().WithMessage("You must supply your card token");
		}
	}
}