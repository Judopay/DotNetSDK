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
                .NotEmpty().WithMessage("You must supply the amount to collect")
                .GreaterThanOrEqualTo(0.01M).WithMessage("Sorry, this refund amount is not valid.");

            RuleFor(model => model.YourPaymentReference)
                .NotEmpty().WithMessage("You must supply your payment reference");
        }
    }
}
