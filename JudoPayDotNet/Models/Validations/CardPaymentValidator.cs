using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	public class CardPaymentValidator : PaymentsBaseValidator<CardPaymentModel>
	{
		public CardPaymentValidator()
		{
			RuleFor(model => model.CardNumber)
				.NotEmpty().WithMessage("You must supply your card number");

			RuleFor(model => model.ExpiryDate)
				.NotEmpty().WithMessage("You must supply your card expiry date");
		}
	}
}