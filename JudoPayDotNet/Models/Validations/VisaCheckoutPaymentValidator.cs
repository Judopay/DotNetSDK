using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
    internal class VisaCheckoutPaymentValidator : PaymentsBaseValidator<VisaCheckoutPaymentModel>
    {
        public VisaCheckoutPaymentValidator()
        {
            RuleFor(model => model.Wallet)
                .NotEmpty().WithMessage("You must supply a wallet");

            RuleFor(model => model.Wallet.EncryptedPaymentData)
                .NotEmpty().WithMessage("You must supply the Encrypted Payment Data");

            RuleFor(model => model.Wallet.EncryptedKey)
                .NotEmpty().WithMessage("You must supply the Encrypted Key");

            RuleFor(model => model.Wallet.CallId)
                .NotEmpty().WithMessage("You must supply the Call Id");
        }
    }
}