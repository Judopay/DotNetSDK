using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class PaymentRequiresThreeDSecureTwoModelValidator : TransactionResultBaseValidation<PaymentRequiresThreeDSecureTwoModel>
	{
		public PaymentRequiresThreeDSecureTwoModelValidator()
		{
			RuleFor(model => model.MethodUrl)
				.NotEmpty().WithMessage("The response must contain a MethodUrl");

			RuleFor(model => model.Version)
				.NotEmpty().WithMessage("The response must contain a Version");

            RuleFor(model => model.Md)
                .NotEmpty().WithMessage("The response must contain a Md");
        }
	}
}