using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class OneTimePaymentValidator : PaymentsBaseValidator<OneTimePaymentModel>
	{
		public OneTimePaymentValidator()
		{
			RuleFor(model => model.OneUseToken)
				.NotEmpty().WithMessage("You must supply a one time token");
		}
	}
}