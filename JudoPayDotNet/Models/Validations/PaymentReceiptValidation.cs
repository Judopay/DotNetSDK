using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class PaymentReceiptValidation : TransactionResultBaseValidation<PaymentReceiptModel>
	{
		public PaymentReceiptValidation()
		{
			RuleFor(model => model.Type)
				.NotEmpty().WithMessage("The response must contain an Type");

			RuleFor(model => model.JudoId)
				.NotEmpty().WithMessage("The response must contain an Judo Id");
		}
	}
}