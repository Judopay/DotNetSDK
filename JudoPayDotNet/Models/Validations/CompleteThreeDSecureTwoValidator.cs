using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
// ReSharper disable UnusedMember.Global
	internal class CompleteThreeDSecureTwoValidator : AbstractValidator<CompleteThreeDSecureTwoModel>
// ReSharper restore UnusedMember.Global
    {
        public CompleteThreeDSecureTwoValidator()
        {
            RuleFor(model => model.CV2)
                .NotEmpty().WithMessage("You must supply your card CV2");
        }
    }
}
