using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class PaymentRequiresThreeDSecureTwoModelValidation : TransactionResultBaseValidation<PaymentRequiresThreeDSecureTwoModel>
	{
		public PaymentRequiresThreeDSecureTwoModelValidation()
		{
			RuleFor(model => model.MethodUrl)
				.NotEmpty().WithMessage("The response must contain a MethodUrl");

			RuleFor(model => model.Md)
				.NotEmpty().WithMessage("The response must contain a Md");

			RuleFor(model => model.Version)
				.NotEmpty().WithMessage("The response must contain a Version");
		}
	}
}