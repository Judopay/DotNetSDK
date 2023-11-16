using FluentValidation;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Validation
{
    internal class PaymentReceiptValidator : AbstractValidator<PaymentReceiptModel>
    {
        public PaymentReceiptValidator()
        {
            RuleFor(model => model.ReceiptId)
                .NotEmpty().WithMessage("The response must contain a receipt ID");

            RuleFor(model => model.Type)
                .NotEmpty().WithMessage("The response must contain an Type");

            RuleFor(model => model.JudoId)
                .NotEmpty().WithMessage("The response must contain an Judo Id");
        }
    }
}
