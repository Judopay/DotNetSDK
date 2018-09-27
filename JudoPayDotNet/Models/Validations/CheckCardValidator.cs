using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
    internal class CheckCardValidator : AbstractValidator<CheckCardModel>
    {
        public CheckCardValidator()
        {
            RuleFor(model => model.YourConsumerReference)
                .NotEmpty().WithMessage("You must supply your unique consumer reference")
                .Length(1, 50).WithMessage("You unique consumer reference needs to be less than 50 characters.");

            RuleFor(model => model.CardNumber)
                .NotEmpty().WithMessage("You must supply your card number");

            RuleFor(model => model.ExpiryDate)
                .NotEmpty().WithMessage("You must supply your card expiry date");
        }
    }
}