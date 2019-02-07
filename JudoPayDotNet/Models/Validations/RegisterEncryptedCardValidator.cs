using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
	internal class RegisterEncryptedCardValidator : AbstractValidator<RegisterEncryptedCardModel>
	{
        public RegisterEncryptedCardValidator()
		{
            RuleFor(model => model.YourConsumerReference)
                .NotEmpty().WithMessage("You must supply your unique consumer reference")
                .Length(1, 50).WithMessage("You unique consumer reference needs to be less than 50 characters.");

			RuleFor(model => model.OneUseToken)
				.NotEmpty().WithMessage("You must supply a One Use Token");
		}
	}
}