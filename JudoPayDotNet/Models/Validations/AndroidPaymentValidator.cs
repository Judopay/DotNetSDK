using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
    internal class AndroidPaymentValidator : PaymentsBaseValidator<AndroidPaymentModel>
    {
        public AndroidPaymentValidator()
        {
            RuleFor(model => model.Wallet)
                .NotEmpty().WithMessage("You must supply a wallet");

            RuleFor(model => model.Wallet.EncryptedMessage)
                .NotEmpty().WithMessage("You must supply the EncryptedMessage");

            RuleFor(model => model.Wallet.EphemeralPublicKey)
                .NotEmpty().WithMessage("You must supply the EphemeralPublicKey");

            RuleFor(model => model.Wallet.Tag)
                .NotEmpty().WithMessage("You must supply the Tag");

            RuleFor(model => model.Wallet.PublicKey)
                .NotEmpty().WithMessage("You must supply the PublicKey");

            RuleFor(model => model.Wallet.InstrumentDetails)
                .NotEmpty().WithMessage("You must supply the InstrumentDetails");

            RuleFor(model => model.Wallet.InstrumentType)
                .NotEmpty().WithMessage("You must supply the InstrumentType");

            RuleFor(model => model.Wallet.GoogleTransactionId)
                .NotEmpty().WithMessage("You must supply the GoogleTransactionId");

            RuleFor(model => model.Wallet.Environment)
                .NotEmpty().WithMessage("You must supply the Environment");

            RuleFor(model => model.Wallet.Version)
                .NotEmpty().WithMessage("You must supply the Version");
        }
    }
}