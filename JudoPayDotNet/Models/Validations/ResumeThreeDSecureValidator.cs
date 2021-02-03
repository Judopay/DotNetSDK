using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
// ReSharper disable UnusedMember.Global
	internal class ResumeThreeDSecureValidator : AbstractValidator<ResumeThreeDSecureModel>
// ReSharper restore UnusedMember.Global
    {
        public ResumeThreeDSecureValidator()
        {
            RuleFor(model => model.CV2)
                .NotEmpty().WithMessage("You must supply your card CV2");

            RuleFor(model => model.ThreeDSecure)
                .NotNull().WithMessage("You must supply a value for the ThreeDSecure object");

            When(model => model.ThreeDSecure != null, () => {
                RuleFor(model => model.ThreeDSecure.MethodCompletion)
                    .NotNull().WithMessage("You must supply a value for methodCompletion");
            });

        }
    }
}
