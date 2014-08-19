using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class PaymentRequiresThreeDSecureModelValidation : TransactionResultBaseValidation<PaymentRequiresThreeDSecureModel>
	{
		public PaymentRequiresThreeDSecureModelValidation()
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