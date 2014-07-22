using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
    public class RefundsValidator : AbstractValidator<RefundModel>
    {
        public RefundsValidator()
        {
            RuleFor(model => model.ReceiptId)
                .NotEmpty().WithMessage("You must supply the receipt id of pre authorization transaction");

            RuleFor(model => model.Amount)
                .NotEmpty().WithMessage("You must supply the amount to collect");

            RuleFor(model => model.YourPaymentReference)
                .NotEmpty().WithMessage("You must supply your payment reference");
        }
    }
}
