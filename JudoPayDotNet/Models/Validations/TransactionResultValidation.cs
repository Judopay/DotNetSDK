using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
    public class TransactionResultBaseValidation<TTransactionResult> : AbstractValidator<TTransactionResult> where TTransactionResult : ITransactionResult
    {
        public TransactionResultBaseValidation()
        {
            RuleFor(model => model.ReceiptId)
                .NotEmpty().WithMessage("The response must contain a receipt ID");
        }
    }

    public class TransactionResulttValidation : TransactionResultBaseValidation<ITransactionResult>
    {
        public TransactionResulttValidation()
        {

        }
    }

    public class PaymentReceiptValidation : TransactionResultBaseValidation<PaymentReceiptModel>
    {
        public PaymentReceiptValidation()
        {
            RuleFor(model => model.Type)
                .NotEmpty().WithMessage("The response must contain an Type");

            RuleFor(model => model.JudoId)
                .NotEmpty().WithMessage("The response must contain an Judo Id");
        }
    }

    public class PaymentRequiresThreeDSecureModelValidation : TransactionResultBaseValidation<PaymentRequiresThreeDSecureModel>
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
