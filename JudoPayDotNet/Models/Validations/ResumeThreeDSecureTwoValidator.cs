using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
// ReSharper disable UnusedMember.Global
	internal class ResumeThreeDSecureTwoValidator : AbstractValidator<ResumeThreeDSecureTwoModel>
// ReSharper restore UnusedMember.Global
    {
        public ResumeThreeDSecureTwoValidator()
        {
            RuleFor(model => model.CV2)
                .NotEmpty().WithMessage("You must supply your card CV2");

            RuleFor(model => model.MethodCompletion)
                .NotNull().WithMessage("You must supply a value for methodCompletion");
        }
    }
}
