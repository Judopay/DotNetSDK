using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
// ReSharper disable UnusedMember.Global
    public class ThreeDResultValidator : AbstractValidator<ThreeDResultModel>
// ReSharper restore UnusedMember.Global
    {
        public ThreeDResultValidator()
        {
            RuleFor(model => model.PaRes)
                .NotEmpty().WithMessage("You must provide the PaRes");
        }
    }
}
