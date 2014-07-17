using FluentValidation;

namespace JudoPayDotNet.Models.Validations
{
    public class ThreeDResultValidator : AbstractValidator<ThreeDResultModel>
    {
        public ThreeDResultValidator()
        {
            RuleFor(model => model.PaRes)
                .NotEmpty().WithMessage("You must provide the PaRes");
        }
    }
}
