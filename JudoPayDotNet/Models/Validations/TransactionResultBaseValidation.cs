using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	public class TransactionResultBaseValidation<TTransactionResult> : AbstractValidator<TTransactionResult> where TTransactionResult : ITransactionResult
	{
		protected TransactionResultBaseValidation()
		{
			RuleFor(model => model.ReceiptId)
				.NotEmpty().WithMessage("The response must contain a receipt ID");
		}
	}
}