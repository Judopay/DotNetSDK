using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class PaymentRequiresThreeDSecureModelValidator : TransactionResultBaseValidation<PaymentRequiresThreeDSecureModel>
	{
		public PaymentRequiresThreeDSecureModelValidator()
		{
			RuleFor(model => model.AcsUrl)
				.NotEmpty().WithMessage("The response must contain an AcsUrl");

			RuleFor(model => model.Md)
				.NotEmpty().WithMessage("The response must contain a Md");

			RuleFor(model => model.PaReq)
				.NotEmpty().WithMessage("The response must contain a PaReq");
		}
	}
}