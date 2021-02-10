using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
// ReSharper disable UnusedMember.Global
	internal class CompleteThreeDSecureValidator : AbstractValidator<CompleteThreeDSecureTwoModel>
// ReSharper restore UnusedMember.Global
    {
        public CompleteThreeDSecureValidator()
        {
            RuleFor(model => model.CV2)
                .NotEmpty().WithMessage("You must supply your card CV2");

            RuleFor(model => model.Version)
                .NotEmpty().WithMessage("You must supply your response Version as returned by the initial payment call");
        }
    }
}
