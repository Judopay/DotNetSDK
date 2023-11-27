using FluentValidation;
using JudoPayDotNet.Models;

namespace JudoPayDotNet.Validation
{
    internal class PaymentRequiresThreeDSecureTwoValidator : AbstractValidator<PaymentRequiresThreeDSecureTwoModel>
    {
        public PaymentRequiresThreeDSecureTwoValidator()
        {
            RuleFor(model => model.ReceiptId)
                .NotEmpty().WithMessage("The response must contain a receipt ID");

            RuleFor(model => model.MethodUrl)
                .NotEmpty().WithMessage("The response must contain a MethodUrl");

            RuleFor(model => model.Version)
                .NotEmpty().WithMessage("The response must contain a Version");

            RuleFor(model => model.Md)
                .NotEmpty().WithMessage("The response must contain a Md");
        }
    }
}